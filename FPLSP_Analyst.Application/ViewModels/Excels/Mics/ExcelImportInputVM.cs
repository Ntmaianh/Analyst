namespace FPLSP_Analyst.Application.ViewModels.Excels.Mics
{
    public class ExcelImportInputVM
    {
        public string Function { get; set; } = string.Empty;
        public List<ExcelParameterVM> Parameters { get; set; } = new();

        public string FileName { get; set; } = string.Empty;
        public byte[] FileData { get; set; }
    }
}
