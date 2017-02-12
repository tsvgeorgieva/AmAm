using System.Collections.Generic;

namespace NutritionRecommendationEngine.Engine.Meals
{
    class MealFactory
    {
        public static IMeal GetMeal(IEnumerable<DietaryReferenceIntake> dris, IEnumerable<FoodIntake> intakes)
        {
            var knapsack = new Meal(dris);
            knapsack.SetFoodIntakes(intakes);
            return knapsack;
        }
    }
}
