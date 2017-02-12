using NutritionRecommendationEngine.Engine.Crossover;
using NutritionRecommendationEngine.Engine.Initializer;
using NutritionRecommendationEngine.Engine.Mutator;
using NutritionRecommendationEngine.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NutritionRecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AmAmDbContext, Configuration>());

            Console.WriteLine("Hello, I am AmAm! I will recommend what foods you can eat in order to achieve your daily nutritional targets.");
            Console.WriteLine("Available commands:");
            Console.WriteLine("'Start fresh' - clears all previously entered data");
            Console.WriteLine("'I ate' - saves food intake");
            Console.WriteLine("'I like' - saves food you like and recommends only from your liked foods");
            Console.WriteLine("'Give me my meal' - starts calculating the recomendations");
            Console.WriteLine("'debug on/off' - turns debug mode on and off");
            Console.WriteLine("'End' - exits the program");

            var db = new AmAmDbContext();

            var dris = db.DietaryReferenceIntakes.ToList();
            var allFoods = db.Foods.ToList();

            var userDris = dris;
            var userFoods = allFoods;

            var isDebug = false;

            while (true)
            {
                Console.WriteLine("What can I do for you today?");
                var command = Console.ReadLine();
                if (command.Equals("end", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                else if (command.Equals("debug on", StringComparison.InvariantCultureIgnoreCase))
                {
                    isDebug = true;
                }
                else if (command.Equals("debug off", StringComparison.InvariantCultureIgnoreCase))
                {
                    isDebug = false;
                }
                else if (command.Equals("start fresh", StringComparison.InvariantCultureIgnoreCase))
                {
                    userDris = db.DietaryReferenceIntakes.ToList();
                    userFoods = allFoods;
                }
                else if (command.StartsWith("I like", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Enter food name:");
                    var foodName = Console.ReadLine().ToUpperInvariant();
                    var foodsMatching = allFoods.Where(f => f.Name.Contains(foodName)).ToList();
                    
                    foreach (var food in foodsMatching)
                    {
                        Console.WriteLine($"{food.Id} {food.Name}");
                    }
                    Console.WriteLine("Please enter food id:");
                    var foodid = int.Parse(Console.ReadLine());
                    var foodEaten = foodsMatching.FirstOrDefault(f => f.Id == foodid);
                    if (foodEaten == null)
                    {
                        Console.WriteLine("I can't find matches for this search, please try again!");
                        continue;
                    }

                    if(userFoods == allFoods)
                    {
                        userFoods = new List<Food>();
                    }

                    userFoods.Add(foodEaten);
                }
                else if (command.StartsWith("I ate", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Enter food name:");
                    var foodName = Console.ReadLine().ToUpperInvariant();
                    var foodsMatching = allFoods.Where(f => f.Name.Contains(foodName)).ToList();
                    Food foodEaten = null;
                    foreach (var food in foodsMatching)
                    {
                        Console.WriteLine($"Is this the food you ate: {food.Name}? Answer with 'yes' or 'no'");
                        var answer = Console.ReadLine();
                        if (answer.Equals("yes", StringComparison.InvariantCultureIgnoreCase))
                        {
                            foodEaten = food;
                            break;
                        }
                    }

                    if (foodEaten == null)
                    {
                        Console.WriteLine("I can't find matches for this search, please try again!");
                        continue;
                    }

                    Console.WriteLine("Enter food amount in grams:");
                    var intake = double.Parse(Console.ReadLine()) / 100;
                    foreach (var dri in userDris)
                    {
                        var nutrientEaten = foodEaten.GetNutrientValue(dri.NutrientName) * intake;
                        dri.EstimatedAverageRequirement = dri.EstimatedAverageRequirement.HasValue ? (double?)Math.Max(0, dri.EstimatedAverageRequirement.Value - nutrientEaten) : null;
                        dri.RecommendedDietaryAllowance = dri.RecommendedDietaryAllowance.HasValue ? (double?)Math.Max(0, dri.RecommendedDietaryAllowance.Value - nutrientEaten) : null;
                        dri.TolerableUpperIntakeLevel = dri.TolerableUpperIntakeLevel.HasValue ? (double?)Math.Max(0, dri.TolerableUpperIntakeLevel.Value - nutrientEaten) : null;
                    }
                    Console.WriteLine("Saved!");
                }
                else if (command.Equals("give me my meal", StringComparison.InvariantCultureIgnoreCase))
                {
                    Solve(userDris, userFoods, isDebug);
                }
            }

            Console.WriteLine("Bye!");
        }

        static void Solve(IEnumerable<DietaryReferenceIntake> dris, IEnumerable<Food> foods, bool isDebug)
        {
            int iterationCount = 500;
            var solver = new Engine.Engine(new RandomInitializer(), new Crossoverer(), new Mutator(), isDebug);
            var solution = solver.Solve(dris, foods, iterationCount);

            Console.WriteLine();
            Console.WriteLine("Your chosen meal:");
            foreach (var item in solution.FoodIntakes)
            {
                Console.WriteLine($"{item.Intake * 100,3:F0}g {item.Food.Name}");
            }
            Console.WriteLine();
            foreach (var dri in solution.DietaryReferenceIntakes)
            {
                Console.WriteLine($"{dri.NutrientName,-10} Min: {dri.Min,-7:F2} Max: {dri.Max,-7:F2} Meal: {solution.TotalNutrients.GetNutrientValue(dri.NutrientName),-7:F2} Diff: {solution.TotalCost.GetNutrientValue(dri.NutrientName),-7:F2}");
            }
            Console.WriteLine($"Total diff: {solution.TotalCostSum:F2}");
            Console.WriteLine();
        }
    }
}
