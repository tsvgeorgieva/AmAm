namespace NutritionRecommendationEngine
{
    public class DietaryReferenceIntake
    {
        public int Id { get; set; }
        public string NutrientName { get; set; }
        public double? EstimatedAverageRequirement { get; set; }
        public double? RecommendedDietaryAllowance { get; set; }
        public double? TolerableUpperIntakeLevel { get; set; }

        public double? Min => RecommendedDietaryAllowance ?? EstimatedAverageRequirement;
        public double? Max => TolerableUpperIntakeLevel;
    }
}
