using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionRecommendationEngine
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Lipids { get; set; }
        public double Carbs { get; set; }
        public double Calcium { get; set; }
        public double Iron { get; set; }
        public double Zinc { get; set; }
        public double VitaminC { get; set; }
        public double VitaminA { get; set; }
        public double VitaminB12 { get; set; }

        public virtual List<FoodIntake> FoodIntakes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
