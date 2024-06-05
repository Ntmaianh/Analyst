using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelMajorIndicatorVM
    {
        public string MajorName { get; set; }
        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public int StudentBannedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentMissedNumber { get; set; }
        public string StudentPassedPercent { get; set; }
        public string StudentBannedPercent { get; set; }
        public string StudentFailedPercent { get; set; }
        public int SubjectTotalNumber { get; set; }
        public int SubjectTotalNumberBannedHigher20 { get; set; } /// Môn cấm thi trên 20%
        public string SubjectBannedHigher20Percent { get; set; }
        public int SubjectTotalNumberBannedHigher10 { get; set; } /// Môn cấm thi trên 10%
        public string SubjectBannedHigher10Percent { get; set; }
        public int SubjectTotalNumberBannedSmaller3 { get; set; } /// Môn cấm thi dưới 3%
        public string SubjectBannedSmaller3Percent { get; set; }
        public int TotalNumberClass { get; set; }
        public int ClassGreaterThan20PercentBannedNumber { get; set; } /// Lớp cấm thi >20%
        public string ClassGreaterThan20PercentBannedPercent { get; set; }
        public int ClassGreaterThan10PercentBannedNumber { get; set; }  /// Lớp cấm thi >10%
        public string ClassGreaterThan10PercentBannedPercent { get; set; }
        public int ClassLessThan3PercentBannedNumber { get; set; } /// Lớp cấm thi <3%
        public string ClassLessThan3PercentBannedPercent { get; set; }
        public int NeedExplanation { get; set; } // sl cần giải trình 
        public string Explanation { get; set; }
        public string Semester { get; set; }

        public MajorIndicatorEntity ConvertToEntity()
        {
            return new MajorIndicatorEntity()
            {
                StudentTotalNumber = StudentTotalNumber,
                StudentPassedNumber = StudentPassedNumber,
                StudentFailedNumber = StudentFailedNumber,
                StudentPassedPercent = float.Parse(StudentPassedPercent.Replace("%", "")),
                StudentFailedPercent = float.Parse(StudentFailedPercent.Replace("%", "")),
                StudentBannedPercent = float.Parse(StudentBannedPercent.Replace("%", "")),

                // Subject

                SubjectTotalNumber = SubjectTotalNumber,
                SubjectGreaterThan20PercentBannedNumber = SubjectTotalNumberBannedHigher20,
                SubjectGreaterThan10PercentBannedNumber = SubjectTotalNumberBannedHigher10,
                SubjectLessThan3PercentBannedNumber = SubjectTotalNumberBannedSmaller3,
                SubjectGreaterThan20PercentBannedPercent = float.Parse(SubjectBannedHigher20Percent.Replace("%", "")),
                SubjectGreaterThan10PercentBannedPercent = float.Parse(SubjectBannedHigher10Percent.Replace("%", "")),
                SubjectLessThan3PercentBannedPercent = float.Parse(SubjectBannedSmaller3Percent.Replace("%", "")),

                //  Class

                ClassTotalNumber = TotalNumberClass,
                ClassGreaterThan20PercentBannedNumber = ClassGreaterThan20PercentBannedNumber,
                ClassGreaterThan10PercentBannedNumber = ClassGreaterThan10PercentBannedNumber,
                ClassLessThan3PercentBannedNumber = ClassLessThan3PercentBannedNumber,
                ClassGreaterThan20PercentBannedPercent = float.Parse(ClassGreaterThan20PercentBannedPercent.Replace("%", "")),
                ClassGreaterThan10PercentBannedPercent = float.Parse(ClassGreaterThan10PercentBannedPercent.Replace("%", "")),
                ClassLessThan3PercentBannedPercent = float.Parse(ClassLessThan3PercentBannedPercent.Replace("%", "")),

            };
        }
    }
}
