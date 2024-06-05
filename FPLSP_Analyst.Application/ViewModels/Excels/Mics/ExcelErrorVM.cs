namespace FPLSP_Analyst.Application.ViewModels.Excels.Mics
{
    public class ExcelErrorVM
    {
        public int Row { get; set; }
        public int? Col { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
