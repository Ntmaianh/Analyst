namespace FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator
{
    public class LecturerIndicatorForDetails
    {
        public Guid SemesterId { get; set; }
        public Guid LecturerId { get; set; }

        public string Semester { get; set; } = null!;
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
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
    }
}
