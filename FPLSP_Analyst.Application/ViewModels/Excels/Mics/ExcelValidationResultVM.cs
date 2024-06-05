namespace FPLSP_Analyst.Application.ViewModels.Excels.Mics
{
    public class ExcelValidationResultVM
    {
        public List<int> ValidRows { get; set; } = new();
        public MemoryStream? MemoryStream { get; set; }
        public bool IsValid { get; set; } // kiểm tra trong validate đang có lỗi hay không
    }
}
