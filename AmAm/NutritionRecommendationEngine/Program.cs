using NutritionRecommendationEngine.Migrations;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace NutritionRecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AmAmDbContext, Configuration>());

            var db = new AmAmDbContext();
            var food = db.Foods.Where(f => f.Calories < 10).ToList();
            Console.WriteLine(food.Count);
        }
    }
}
