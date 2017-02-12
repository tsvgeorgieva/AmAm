using System;
using System.Collections.Generic;

namespace NutritionRecommendationEngine.Engine.Meals
{
    public interface IMeal : IComparable<IMeal>
    {
        Food TotalCost { get; }
        double TotalCostSum { get; }
        Food TotalNutrients { get; }
        IEnumerable<DietaryReferenceIntake> DietaryReferenceIntakes { get; }
        IList<FoodIntake> FoodIntakes { get; }
        int FoodIntakesCount { get; }
        bool AddFoodIntake(FoodIntake item);
        void Remove(int removeIndex);
        void ChangeIntake(FoodIntake item, double newValue);
        void SetFoodIntakes(IEnumerable<FoodIntake> foodIntakes);
        IEnumerable<FoodIntake> Skip(int skip);
        IEnumerable<FoodIntake> Take(int take);
        void Fill(IEnumerable<Food> items);
    }
}
