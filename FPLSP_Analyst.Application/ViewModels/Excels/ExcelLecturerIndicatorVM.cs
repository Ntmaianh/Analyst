using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelLecturerIndicatorVM
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Major { get; set; }
        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }

        public string StudentPassedPercent { get; set; }
        public string StudentFailedPercent { get; set; }
        public string StudentBannedPercent { get; set; }
        public int ClassTotalNumber { get; set; } //10

        public int ClassGreaterThan20PercentBannedNumber { get; set; } //11
        public string ClassGreaterThan20PercentBannedPercent { get; set; }//12
        public int ClassGreaterThan10PercentBannedNumber { get; set; } //13

        public string ClassGreaterThan10PercentBannedPercent { get; set; }//14
        public int ClassLessThan3PercentBannedNumber { get; set; }//15
        
        public string ClassLessThan3PercentBannedPercent { get; set; }//16
        public string NeedToConsider { get; set; }

        public string Semester { get; set; }

        public LecturerEntity ConvertToEntity()
        {
            return new LecturerEntity()
            {
                Username = Username,
            };
        }
        public LecturerIndicatorEntity ConvertToIndicatorEntity()
        {
            try
            {
                return new LecturerIndicatorEntity()
                {

                    StudentTotalNumber = StudentTotalNumber,
                    StudentPassedNumber = StudentPassedNumber,
                    StudentFailedNumber = StudentFailedNumber,
                    StudentPassedPercent = ConvertExcelValue.PercentValue(StudentPassedPercent),
                    StudentFailedPercent = ConvertExcelValue.PercentValue(StudentFailedPercent),
                    StudentBannedPercent = ConvertExcelValue.PercentValue(StudentBannedPercent),

                    ClassTotalNumber = StudentTotalNumber,
                    ClassGreaterThan20PercentBannedNumber = ClassGreaterThan20PercentBannedNumber,
                    ClassGreaterThan10PercentBannedNumber = ClassGreaterThan10PercentBannedNumber,
                    ClassLessThan3PercentBannedNumber = ClassLessThan3PercentBannedNumber,

                    ClassGreaterThan20PercentBannedPercent = ConvertExcelValue.PercentValue(ClassGreaterThan20PercentBannedPercent),
                    ClassGreaterThan10PercentBannedPercent = ConvertExcelValue.PercentValue(ClassGreaterThan10PercentBannedPercent),
                    ClassLessThan3PercentBannedPercent = ConvertExcelValue.PercentValue(ClassLessThan3PercentBannedPercent)
                };
            }
            catch
            {
                return null;
            }             
        }
    }
}
