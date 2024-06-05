namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorOverview.Details
{
    public class StudentForMajorStatistic
    {
        // Student
        public int StudentTotalNumber { get; set; }

        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }
        public int StudentMissedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }
    }
}
