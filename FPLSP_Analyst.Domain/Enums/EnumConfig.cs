namespace FPLSP_Analyst.Domain.Enums
{
    public class EnumConfig
    {
        public enum Indicator
        {
            StudentPassedPercent = 0,
            StudentFailedPercent = 1,
            StudentBannedPercent = 2
        }

        public enum TableName
        {
            MajorIndicator = 1,
            LecturerIndicator = 2,
            ClassIndicator = 3,
            SubjectIndicator = 4,
        }

        public enum ColumnName
        {
            StudentTotalNumber = 0,
            StudentPassedNumber = 1,
            StudentFailedNumber = 2,
            StudentBannedNumber = 3,
            StudentPassedPercent = 4,
            StudentFailedPercent = 5,
            StudentBannedPercent = 6,
            ClassTotalNumber = 7,
            ClassGreaterThan20PercentBannedNumber = 8,
            ClassGreaterThan10PercentBannedNumber = 9,
            ClassLessThan3PercentBannedNumber = 10,
            ClassGreaterThan20PercentBannedPercent = 11,
            ClassGreaterThan10PercentBannedPercent = 12,
            ClassLessThan3PercentBannedPercent = 13,
            StudentMissedNumber = 14,
        }
    }
}
