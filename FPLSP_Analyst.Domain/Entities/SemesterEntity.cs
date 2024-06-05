using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class SemesterEntity : EntityBase
    {
        public Guid Id { get; set; }
        public Guid GroupConfigId { get; set; }
        public string Code { get; set; } = null!;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public virtual GroupConfigEntity? GroupConfig { get; set; }

        public virtual ICollection<MajorIndicatorEntity>? MajorIndicators { get; set; }
        public virtual ICollection<LecturerIndicatorEntity>? LecturerIndicators { get; set; }
        public virtual ICollection<SubjectIndicatorEntity>? SubjectIndicators { get; set; }
        public virtual ICollection<ClassIndicatorEntity>? ClassIndicators { get; set; }
    }
}
