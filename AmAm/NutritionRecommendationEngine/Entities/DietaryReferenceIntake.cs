using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionRecommendationEngine
{
    public class DietaryReferenceIntake
    {
        public int Id { get; set; }
        public string NutrientName { get; set; }
        public double? EstimatedAverageRequirement { get; set; }
        public double? RecommendedDietaryAllowance { get; set; }
        public double? TolerableUpperIntakeLevel { get; set; }
    }
}
