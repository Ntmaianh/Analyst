using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class ClassIndicatorEntity : EntityBase
    {
        public Guid Id { get; set; }
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid LecturerId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }

        public virtual SemesterEntity? Semester { get; set; }
        public virtual SubjectEntity? Subject { get; set; }
        public virtual LecturerEntity? Lecturer { get; set; }
    }
}
