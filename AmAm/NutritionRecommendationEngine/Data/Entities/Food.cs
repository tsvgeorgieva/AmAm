using System.Reflection;

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

        public double GetNutrientValue(string nutrientName)
        {
            return (double)GetType().GetProperty(nutrientName).GetValue(this, null);
        }

        public void SetNutrientValue(string nutrientName, double value)
        {
            var prop = GetType().GetProperty(nutrientName);
            prop.SetValue(this, value, null);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
