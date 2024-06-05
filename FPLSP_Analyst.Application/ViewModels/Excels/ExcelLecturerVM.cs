using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelLecturerVM
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Major { get; set; }

        public LecturerEntity ConvertToEntity()
        {
            return new LecturerEntity()
            {
                Username = UserName,
            };
        }
    }
}
