using System.Collections.Generic;

namespace NutritionRecommendationEngine.Engine.Initializer
{
    public interface IInitializer
    {
        Population InitializePopulation(IEnumerable<DietaryReferenceIntake> dris, IEnumerable<Food> foods);
    }
}
