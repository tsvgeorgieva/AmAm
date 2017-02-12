namespace NutritionRecommendationEngine.Migrations
{
    using OfficeOpenXml;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AmAmDbContext>
    {
        private static readonly int CalciumColumnNum = 11;
        private static readonly int CaloriesColumnNum = 4;
        private static readonly int CarbsColumnNum = 8;
        private static readonly int IronColumnNum = 12;
        private static readonly int LipidsColumnNum = 6;
        private static readonly int ProteinsColumnNum = 5;
        private static readonly int VitaminAColumnNum = 32;
        private static readonly int VitaminB12ColumnNum = 31;
        private static readonly int VitaminCColumnNum = 21;
        private static readonly int ZincColumnNum = 17;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NutritionRecommendationEngine.AmAmDbContext";
        }

        protected override void Seed(AmAmDbContext context)
        {
            //using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"D:\projects\AmAm\AmAm\NutritionRecommendationEngine\NutritionData.xlsx")))
            //{
            //    var dataSheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
            //    var totalRows = dataSheet.Dimension.End.Row;
            //    var totalColumns = dataSheet.Dimension.End.Column;

            //    var nutrientsRow = dataSheet.Cells[1, 1, 1, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();

            //    using (var db = new AmAmDbContext())
            //    {
            //        for (int row = 2; row <= totalRows; row++) //selet starting row here
            //        {
            //            var food = new Food()
            //            {
            //                Id = int.Parse(dataSheet.GetCell(row, 1)),
            //                Name = dataSheet.GetCell(row, 2),
            //                Calcium = dataSheet.GetCellAsDouble(row, CalciumColumnNum),
            //                Calories = dataSheet.GetCellAsDouble(row, CaloriesColumnNum),
            //                Carbs = dataSheet.GetCellAsDouble(row, CarbsColumnNum),
            //                Iron = dataSheet.GetCellAsDouble(row, IronColumnNum),
            //                Lipids = dataSheet.GetCellAsDouble(row, LipidsColumnNum),
            //                Proteins = dataSheet.GetCellAsDouble(row, ProteinsColumnNum),
            //                VitaminA = dataSheet.GetCellAsDouble(row, VitaminAColumnNum),
            //                VitaminB12 = dataSheet.GetCellAsDouble(row, VitaminB12ColumnNum),
            //                VitaminC = dataSheet.GetCellAsDouble(row, VitaminCColumnNum),
            //                Zinc = dataSheet.GetCellAsDouble(row, ZincColumnNum),
            //            };

            //            db.Foods.Add(food);
            //        }
            //        db.SaveChanges();
            //    }

            //}
        }
    }

    public static class Ext
    {
        public static string GetCell(this ExcelWorksheet worksheet, int row, int col)
        {
            return worksheet.Cells[row, col, row, col].First().Value?.ToString();
        }
        public static double GetCellAsDouble(this ExcelWorksheet worksheet, int row, int col)
        {
            return double.Parse(worksheet.GetCell(row, col) ?? "0");
        }
    }
}
