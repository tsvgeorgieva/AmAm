namespace NutritionRecommendationEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DietaryReferenceIntakes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NutrientName = c.Int(nullable: false),
                        EstimatedAverageRequirement = c.Double(nullable: false),
                        RecommendedDietaryAllowance = c.Double(nullable: false),
                        TolerableUpperIntakeLevel = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodIntakes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodId = c.Int(nullable: false),
                        Intake = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.FoodId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Calories = c.Double(nullable: false),
                        Proteins = c.Double(nullable: false),
                        Lipids = c.Double(nullable: false),
                        Carbs = c.Double(nullable: false),
                        Calcium = c.Double(nullable: false),
                        Iron = c.Double(nullable: false),
                        Zinc = c.Double(nullable: false),
                        VitaminC = c.Double(nullable: false),
                        VitaminA = c.Double(nullable: false),
                        VitaminB12 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodIntakes", "FoodId", "dbo.Foods");
            DropIndex("dbo.FoodIntakes", new[] { "FoodId" });
            DropTable("dbo.Foods");
            DropTable("dbo.FoodIntakes");
            DropTable("dbo.DietaryReferenceIntakes");
        }
    }
}
