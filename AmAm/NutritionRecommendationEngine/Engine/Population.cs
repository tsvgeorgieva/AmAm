using NutritionRecommendationEngine.Engine.Meals;
using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace NutritionRecommendationEngine.Engine
{
    public class Population
    {
        public int Count { get; set; }
        private double RemovePart;
        public int RemoveCount;
        public int KeepCount { get; set; }
        public IEnumerable<DietaryReferenceIntake> DietaryReferenceIntakes { get; set; }
        public IEnumerable<Food> Foods { get; }
        public OrderedBag<IMeal> SortedMeals { get; private set; }
        public OrderedBag<IMeal> Children { get; private set; }

        public Population(IEnumerable<DietaryReferenceIntake> dris, IEnumerable<Food> foods, int count = 10, double removePart = 0.2)
        {
            Count = count;
            RemovePart = removePart;
            RemoveCount = (int)(RemovePart * Count);
            KeepCount = count - RemoveCount;
            SortedMeals = new OrderedBag<IMeal>();
            Children = new OrderedBag<IMeal>();
            DietaryReferenceIntakes = dris;
            Foods = foods;
        }

        public void Add(IMeal knapsack)
        {
            if (Count < (SortedMeals.Count + Children.Count))
            {
                return;
            }
            Children.Add(knapsack);
        }

        public IMeal GetRandomMember()
        {
            return SortedMeals.ElementAt(MyRandom.Next(SortedMeals.Count));
        }

        public IMeal GetRandomChild()
        {
            return Children.ElementAt(MyRandom.Next(Children.Count));
        }

        public void RemoveNotFit()
        {
            if (RemovePart <= 0.5) //todo
            {
                var removeKeys = SortedMeals.Skip(KeepCount).ToList(); // first is best
                foreach (var key in removeKeys)
                {
                    SortedMeals.Remove(key);
                }
            }
            else
            {
                var keepKeys = SortedMeals.Take(KeepCount).ToList();  // last is worst
                var newSortedKnapsacks = new OrderedBag<IMeal>();
                foreach (var key in keepKeys)
                {
                    newSortedKnapsacks.Add(key);
                }
                SortedMeals = newSortedKnapsacks;
            }
        }

        public void MergeNewGeneration()
        {
            SortedMeals.UnionWith(Children);
            if (SortedMeals.Count != Count)
            {
                throw new Exception();
            }
            Children = new OrderedBag<IMeal>();
        }

        public IMeal GetFittest()
        {
            return SortedMeals.First();
        }
    }
}
