namespace FPLSP_Analyst.Application.ViewModels.Excels.Mics
{
    public class ExcelOutputVM
    {
        public bool IsSuccess { get; set; }
        public string? FileName { get; set; }
        public bool IsError { get; set; } // kiểm tra trong worksheet có dòng đỏ lỗi thì lên view cũng sẽ báo đỏ 
    }
}
