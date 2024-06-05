namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorStatistic.Details
{
    public class LecturerIndicatorForMajorStatistic
    {

        public int StudentTotalNumber { get; set; }

        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }

        public int ClassTotalNumber { get; set; }

        public int ClassGreaterThan20PercentBannedNumber { get; set; }
        public int ClassGreaterThan10PercentBannedNumber { get; set; }
        public int ClassLessThan3PercentBannedNumber { get; set; }

        public float ClassGreaterThan20PercentBannedPercent { get; set; }
        public float ClassGreaterThan10PercentBannedPercent { get; set; }
        public float ClassLessThan3PercentBannedPercent { get; set; }
        public string Semester { get; set; }
    }
}
