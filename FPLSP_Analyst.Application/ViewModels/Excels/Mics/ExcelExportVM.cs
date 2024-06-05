namespace FPLSP_Analyst.Application.ViewModels.Excels.Mics
{
    public class ExcelExportVM
    {
        public string Function { get; set; } = string.Empty;
        public List<ExcelParameterVM> Parameters { get; set; } = new();
        public bool IsTemplate { get; set; } = false;
    }
}
