using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class MajorEntity : EntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public virtual ICollection<MajorIndicatorEntity>? MajorIndicators { get; set; }
        public virtual ICollection<LecturerEntity>? Lecturers { get; set; }
        public virtual ICollection<SubjectEntity>? Subjects { get; set; }
    }
}
