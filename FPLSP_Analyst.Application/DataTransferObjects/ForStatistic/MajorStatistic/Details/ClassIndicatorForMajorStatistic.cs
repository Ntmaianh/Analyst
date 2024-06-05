namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorStatistic.Details
{
    public class ClassIndicatorForMajorStatistic
    {
        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }
        public string Semester { get; set; }
    }
}
