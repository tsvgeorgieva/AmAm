using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionRecommendationEngine
{
    public class AmAmDbContext : DbContext
    {
        public AmAmDbContext() : base("AmAmDbContext")
        {

        }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodIntake> FoodIntakes { get; set; }
        public DbSet<DietaryReferenceIntake> DietaryReferenceIntakes { get; set; }
    }
}
