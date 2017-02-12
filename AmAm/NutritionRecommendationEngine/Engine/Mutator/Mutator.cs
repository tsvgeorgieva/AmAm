using NutritionRecommendationEngine.Engine.Meals;

namespace NutritionRecommendationEngine.Engine.Mutator
{
    public class Mutator : IMutator
    {
        private static double MutatePart = 0.5;
        private static int MutateCount;

        public void MutatePopulation(Population population)
        {
            MutateCount = (int)(population.Children.Count * MutatePart);
            for (int i = 0; i < MutateCount; i++)
            {
                var knapsack = population.GetRandomChild();
                Mutate(knapsack, population);
            }
        }

        private void Mutate(IMeal knapsack, Population population)
        {
            if (knapsack.FoodIntakesCount > 0)
            {
                var removeIndex = MyRandom.Next(knapsack.FoodIntakesCount);
                knapsack.Remove(removeIndex);
            }

            knapsack.Fill(population.Foods);
        }
    }
}
