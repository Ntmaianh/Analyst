namespace FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator
{
    public class SubjectIndicatorForDetails
    {
        public string Semester { get; set; }
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
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
