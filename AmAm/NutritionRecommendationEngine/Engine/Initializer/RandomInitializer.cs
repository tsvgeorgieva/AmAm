using NutritionRecommendationEngine.Engine.Meals;
using System.Collections.Generic;

namespace NutritionRecommendationEngine.Engine.Initializer
{
    class RandomInitializer : IInitializer
    {
        public Population InitializePopulation(IEnumerable<DietaryReferenceIntake> dris, IEnumerable<Food> foods)
        {
            var population = new Population(dris, foods);
            for (int i = 0; i < population.Count; i++)
            {
                var meal = GetMeal(dris, foods);
                population.Add(meal);
            }

            population.MergeNewGeneration();

            return population;
        }

        private IMeal GetMeal(IEnumerable<DietaryReferenceIntake> dris, IEnumerable<Food> foods)
        {
            var meal = MealFactory.GetMeal(dris, new FoodIntake[0]);
            meal.Fill(foods);

            return meal;
        }
    }
}
