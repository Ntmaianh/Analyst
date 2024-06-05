namespace FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator
{
    public class ClassIndicatorForDetails
    {
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid LecturerId { get; set; }
        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public string Semester { get; set; } = null!;
    }
}
