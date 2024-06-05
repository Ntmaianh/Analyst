using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class SubjectEntity : EntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string TrainingType { get; set; } = null!;
        public Guid MajorId { get; set; }
        public virtual MajorEntity? Major { get; set; }
        public virtual ICollection<SubjectIndicatorEntity>? SubjectIndicators { get; set; }
        public virtual ICollection<ClassIndicatorEntity>? ClassIndicators { get; set; }
    }
}
