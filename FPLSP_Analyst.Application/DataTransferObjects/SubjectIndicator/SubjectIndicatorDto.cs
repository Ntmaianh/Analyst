namespace FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator
{
    public class SubjectIndicatorDto
    {

        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string MajorName { get; set; }
        public int StudentTotalNumber { get; set; }

        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }
        public int StudentMissedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }

        public bool IsNeedExplanation { get; set; }

        public string? Explanation { get; set; }

        public string SemesterCode { get; set; }

        public Guid SemesterId { get; set; }
        public Guid MajorId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
