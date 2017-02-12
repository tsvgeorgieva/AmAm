using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace NutritionRecommendationEngine.Engine.Meals
{
    public class Meal : IMeal
    {
        public Meal(IEnumerable<DietaryReferenceIntake> dris)
        {
            DietaryReferenceIntakes = dris;
            FoodIntakes = new List<FoodIntake>();
            TotalCost = new Food();
            TotalNutrients = new Food();
        }

        public Food TotalCost { get; set; }
        public double TotalCostSum { get; set; }
        public Food TotalNutrients { get; }
        public IEnumerable<DietaryReferenceIntake> DietaryReferenceIntakes { get; }
        public IList<FoodIntake> FoodIntakes { get; private set; }
        public int FoodIntakesCount => FoodIntakes.Count;

        public bool AddFoodIntake(FoodIntake item)
        {
            var existing = FoodIntakes.FirstOrDefault(fi => fi.Food.Id == item.Food.Id);
            if (existing != null)
            {
                return false;
                //existing.Intake = Math.Min(4, existing.Intake + item.Intake);
            }
            else
            {
                FoodIntakes.Add(item);
            }
            RecalculateNutrients();
            return true;
        }

        public void Remove(int removeIndex)
        {
            FoodIntakes.RemoveAt(removeIndex);
            RecalculateNutrients();
        }

        public void SetFoodIntakes(IEnumerable<FoodIntake> foodIntakes)
        {
            FoodIntakes = foodIntakes.ToList();
            RecalculateNutrients();
        }

        public IEnumerable<FoodIntake> Skip(int skip)
        {
            return FoodIntakes.Skip(skip);
        }

        public IEnumerable<FoodIntake> Take(int take)
        {
            return FoodIntakes.Take(take);
        }

        public int CompareTo(IMeal other)
        {
            //bool bigger = DietaryReferenceIntakes.All(dri => TotalCost.GetNutrientValue(dri.NutrientName) > other.TotalCost.GetNutrientValue(dri.NutrientName));
            //if (bigger)
            //{
            //    return -1;
            //}

            //bool smaller = DietaryReferenceIntakes.All(dri => TotalCost.GetNutrientValue(dri.NutrientName) < other.TotalCost.GetNutrientValue(dri.NutrientName));
            //if (smaller)
            //{
            //    return 1;
            //}
            if (TotalCostSum == 0)
            {
                throw new Exception();
            }

            if (TotalCostSum == other.TotalCostSum)
            {
                return base.Equals(other) ? 0 : GetHashCode().CompareTo(other.GetHashCode());
            }

            return TotalCostSum.CompareTo(other.TotalCostSum);
        }

        public void Fill(IEnumerable<Food> foods)
        {
            var food = foods.GetRandomMember();
            var intake = MyRandom.Next(0.1, 4); // from 10g to 400g
            AddFoodIntake(new FoodIntake
            {
                Food = food,
                Intake = intake
            });
        }

        private void RecalculateNutrients()
        {
            TotalCostSum = 0;
            foreach (var dri in DietaryReferenceIntakes)
            {
                TotalNutrients.SetNutrientValue(dri.NutrientName, FoodIntakes.Sum(f => f.Food.GetNutrientValue(dri.NutrientName) * f.Intake));

                var nutValue = TotalNutrients.GetNutrientValue(dri.NutrientName);
                if (nutValue == 0) nutValue = 0.000001;
                double diff = 0;

                diff = Math.Abs(dri.Min.Value / nutValue - 1);

                if (nutValue > dri.Max)
                {
                    diff += nutValue / dri.Max.Value;
                }

                TotalCost.SetNutrientValue(dri.NutrientName, diff);
                TotalCostSum += diff;
            }
        }

        public void ChangeIntake(FoodIntake item, double newValue)
        {
            FoodIntakes.Remove(item);
            FoodIntakes.Add(new FoodIntake
            {
                Food = item.Food,
                Intake = newValue
            });
            RecalculateNutrients();
        }
    }
}
