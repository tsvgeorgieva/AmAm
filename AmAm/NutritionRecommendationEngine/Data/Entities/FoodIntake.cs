namespace NutritionRecommendationEngine
{
    public class FoodIntake
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public virtual Food Food { get; set; }
        public double Intake { get; set; }
    }
}
