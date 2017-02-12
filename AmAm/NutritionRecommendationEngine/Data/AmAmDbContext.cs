using System.Data.Entity;

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
