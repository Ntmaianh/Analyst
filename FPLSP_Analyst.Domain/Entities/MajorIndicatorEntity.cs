using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class MajorIndicatorEntity : EntityBase
    {
        public Guid Id { get; set; }
        public Guid SemesterId { get; set; }
        public Guid MajorId { get; set; }
        // Student
        public int StudentTotalNumber { get; set; }

        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }
        public int StudentMissedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }

        // Subject
        public int SubjectTotalNumber { get; set; }

        public int SubjectGreaterThan20PercentBannedNumber { get; set; }
        public int SubjectGreaterThan10PercentBannedNumber { get; set; }
        public int SubjectLessThan3PercentBannedNumber { get; set; }

        public float SubjectGreaterThan20PercentBannedPercent { get; set; }
        public float SubjectGreaterThan10PercentBannedPercent { get; set; }
        public float SubjectLessThan3PercentBannedPercent { get; set; }

        // Class
        public int ClassTotalNumber { get; set; }

        public int ClassGreaterThan20PercentBannedNumber { get; set; }
        public int ClassGreaterThan10PercentBannedNumber { get; set; }
        public int ClassLessThan3PercentBannedNumber { get; set; }

        public float ClassGreaterThan20PercentBannedPercent { get; set; }
        public float ClassGreaterThan10PercentBannedPercent { get; set; }
        public float ClassLessThan3PercentBannedPercent { get; set; }

        public virtual SemesterEntity? Semester { get; set; }
        public virtual MajorEntity? Major { get; set; }
    }
}
