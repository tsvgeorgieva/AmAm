using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
