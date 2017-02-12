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

            int iterationCount = 500;
            var dris = db.DietaryReferenceIntakes.ToList();
            var foods = db.Foods.ToList();
            var solver = new Engine.Engine(new RandomInitializer(), new Crossoverer(), new Mutator());
            var solution = solver.Solve(dris, foods, iterationCount);
            Console.WriteLine($"{solution.TotalCostSum:F2}");
            Console.WriteLine();
            Console.WriteLine("Your chosen meal:");
            foreach (var item in solution.FoodIntakes)
            {
                Console.WriteLine($"{item.Intake * 100:F0}g {item.Food.Name}");
            }
            Console.WriteLine();
            foreach (var dri in solution.DietaryReferenceIntakes)
            {
                Console.WriteLine($"{dri.NutrientName,-10} Min: {dri.Min,-7:F2} Max: {dri.Max,-7:F2} Meal: {solution.TotalNutrients.GetNutrientValue(dri.NutrientName),-7:F2} Diff: {solution.TotalCost.GetNutrientValue(dri.NutrientName),-7:F2}");
            }
        }
    }
}
