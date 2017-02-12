using NutritionRecommendationEngine.Engine.Crossover;
using NutritionRecommendationEngine.Engine.Initializer;
using NutritionRecommendationEngine.Engine.Mutator;
using NutritionRecommendationEngine.Migrations;
using System;
using System.Data.Entity;
using System.Linq;

namespace NutritionRecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AmAmDbContext, Configuration>());

            var db = new AmAmDbContext();
            //var food = db.Foods.Where(f => f.Calories < 10).ToList();
            //Console.WriteLine(food.Count);

            int iterationCount = 500;
            var dris = db.DietaryReferenceIntakes.ToList();
            var foods = db.Foods.ToList();
            var solver = new Engine.Engine(new RandomInitializer(), new Crossoverer(), new Mutator());
            var solution = solver.Solve(dris, foods, iterationCount);
            Console.WriteLine(solution.TotalCostSum);
            foreach (var item in solution.FoodIntakes)
            {
                Console.WriteLine($"{item.Intake * 100:F0}g {item.Food.Name}");
            }
            foreach (var dri in solution.DietaryReferenceIntakes)
            {
                Console.WriteLine($"{dri.NutrientName} Min: {dri.Min:F2} Max: {dri.Max:F2} Meal: {solution.TotalNutrients.GetNutrientValue(dri.NutrientName):F2}");
            }
        }
    }
}
