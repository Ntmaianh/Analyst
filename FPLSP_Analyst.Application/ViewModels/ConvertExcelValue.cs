namespace FPLSP_Analyst.Application.ViewModels
{
    public static class ConvertExcelValue
    {
        public static float PercentValue(string floatString)
        {
            var floatFomarted = floatString.Replace(",", ".");

            float floatNumber;

            if (float.TryParse(floatFomarted, out floatNumber))
            {
                floatNumber = floatNumber * 100;
            }
            else
            {
                var floatFomarted2 = floatFomarted.Replace("%", "");
                floatNumber = float.Parse(floatFomarted2);
            }

            return floatNumber;
        }
    }
}
