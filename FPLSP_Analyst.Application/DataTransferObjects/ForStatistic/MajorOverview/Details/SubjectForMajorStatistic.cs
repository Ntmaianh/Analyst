namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorOverview.Details
{
    public class SubjectForMajorStatistic
    { // Subject
        public int SubjectTotalNumber { get; set; }

        public int SubjectGreaterThan20PercentBannedNumber { get; set; }
        public int SubjectGreaterThan10PercentBannedNumber { get; set; }
        public int SubjectLessThan3PercentBannedNumber { get; set; }

        public float SubjectGreaterThan20PercentBannedPercent { get; set; }
        public float SubjectGreaterThan10PercentBannedPercent { get; set; }
        public float SubjectLessThan3PercentBannedPercent { get; set; }
    }
}
