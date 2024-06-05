using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelMajorVM
    {
        public string? BoMon { get; set; }
        public MajorEntity ConvertToEntity()
        {
            return new MajorEntity()
            {
                Code = BoMon!
            };
        }
    }
}
