using NutritionRecommendationEngine.Engine.Meals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionRecommendationEngine.Engine.Crossover
{
    public class Crossoverer : ICrossover
    {
        public void CrossoverPopulation(Population population)
        {
            for (int i = 0; i < population.RemoveCount / 2; i++)
            {
                var firstParent = population.GetRandomMember();
                var secondParent = population.GetRandomMember();

                var children = Crossover(firstParent, secondParent, population.DietaryReferenceIntakes, population.Foods);

                population.Add(children[0]);
                population.Add(children[1]);
            }
        }

        private IMeal[] Crossover(IMeal firstParent, IMeal secondParent, IEnumerable<DietaryReferenceIntake> maxWeight, IEnumerable<Food> foods)
        {
            int i = MyRandom.Next(Math.Min(firstParent.FoodIntakesCount, secondParent.FoodIntakesCount));
            var firstParentPart1 = firstParent.Take(i);
            var secondParentPart2 = secondParent.Skip(i);
            var firstParentPart2 = firstParent.Skip(i);
            var secondParentPart1 = secondParent.Take(i);

            var child1 = FillChildKnapsack(maxWeight, firstParentPart1, secondParentPart1, foods);
            var child2 = FillChildKnapsack(maxWeight, secondParentPart2, firstParentPart2, foods);
            return child1.CompareTo(child2) >= 0 ? new IMeal[] { child1, child2 } : new IMeal[] { child2, child1 };
        }

        private IMeal FillChildKnapsack(IEnumerable<DietaryReferenceIntake> maxWeight, IEnumerable<FoodIntake> firstFoodIntakes, IEnumerable<FoodIntake> secondFoodIntakes, IEnumerable<Food> foods)
        {
            var child = MealFactory.GetMeal(maxWeight, firstFoodIntakes);
            foreach (var item in secondFoodIntakes)
            {
                var random = MyRandom.Next(0, 1);
                if (random < 0.8)
                {
                    child.AddFoodIntake(item);
                }
            }

            if (child.FoodIntakesCount == 0)
            {
                child.Fill(foods);
            }

            return child;
        }
    }
}
