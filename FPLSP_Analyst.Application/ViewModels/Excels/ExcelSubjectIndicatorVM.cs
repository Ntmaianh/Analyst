using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelSubjectIndicatorVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TrainingType { get; set; }
        public string? BoMon { get; set; }
        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public string StudentPassedPercent { get; set; }
        public int StudentBannedNumber { get; set; }
        public string StudentBannedPercent { get; set; }
        public int StudentFailedNumber { get; set; }
        public string StudentFailedPercent { get; set; }
        public int StudentMissedNumber { get; set; }
        public int NeedExplanation { get; set; } // sl cần giải trình 
        public string Explanation { get; set; } //  giải trình 
        public string Semester { get; set; }

        public SubjectIndicatorEntity ConvertToEntity()
        {
            return new SubjectIndicatorEntity()
            {
                StudentTotalNumber = StudentTotalNumber,
                StudentPassedNumber = StudentPassedNumber,
                StudentFailedNumber = StudentFailedNumber,
                StudentPassedPercent = float.Parse(StudentPassedPercent.Replace("%", "")),
                StudentFailedPercent = float.Parse(StudentFailedPercent.Replace("%", "")),
                StudentBannedPercent = float.Parse(StudentBannedPercent.Replace("%", "")),
            };
        }

    }
}
