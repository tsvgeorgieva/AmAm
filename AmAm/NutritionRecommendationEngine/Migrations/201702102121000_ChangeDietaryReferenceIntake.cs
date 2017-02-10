using System.Data.Entity.Migrations;

namespace NutritionRecommendationEngine.Migrations
{
    class AddDietaryReferenceIntake : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DietaryReferenceIntakes", "NutrientName", c => c.String());
            AlterColumn("dbo.DietaryReferenceIntakes", "EstimatedAverageRequirement", c => c.Double(nullable: true));
            AlterColumn("dbo.DietaryReferenceIntakes", "RecommendedDietaryAllowance", c => c.Double(nullable: true));
            AlterColumn("dbo.DietaryReferenceIntakes", "TolerableUpperIntakeLevel", c => c.Double(nullable: true));
        }

        public override void Down()
        {
            AlterColumn("dbo.DietaryReferenceIntakes", "NutrientName", c => c.Int());
            AlterColumn("dbo.DietaryReferenceIntakes", "EstimatedAverageRequirement", c => c.Double(nullable: false));
            AlterColumn("dbo.DietaryReferenceIntakes", "RecommendedDietaryAllowance", c => c.Double(nullable: false));
            AlterColumn("dbo.DietaryReferenceIntakes", "TolerableUpperIntakeLevel", c => c.Double(nullable: false));
        }
    }
}
