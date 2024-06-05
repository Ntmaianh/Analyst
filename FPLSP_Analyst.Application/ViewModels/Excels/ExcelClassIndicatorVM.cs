using FPLSP_Analyst.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelClassIndicatorVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string TrainingType { get; set; }
        public string Lecturer { get; set; }
        public string Major { get; set; }

        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public string StudentPassedPercent { get; set; }
        public int StudentBannedNumber { get; set; }
        public string StudentBannedPercent { get; set; }
        public int StudentFailedNumber { get; set; }
        public string StudentFailedPercent { get; set; }
        public int NeedExplanation { get; set; } // sl cần giải trình 
        public string Explanation { get; set; } //  giải trình 
        public string Semester { get; set; }

        public ClassIndicatorEntity ConvertToEntity()
        {

            try
            {
                return new ClassIndicatorEntity()
                {
                    Code = Code,
                    Name = Name,
                    StudentTotalNumber = StudentTotalNumber,
                    StudentPassedNumber = StudentPassedNumber,
                    StudentFailedNumber = StudentFailedNumber,
                    StudentPassedPercent = ConvertExcelValue.PercentValue(StudentPassedPercent),
                    StudentFailedPercent = ConvertExcelValue.PercentValue(StudentFailedPercent),
                    StudentBannedPercent = ConvertExcelValue.PercentValue(StudentBannedPercent),
                };
            }
            catch (Exception ex)
            {               
                return null; // Return null or handle the error case appropriately
            }
        }
    }
}
