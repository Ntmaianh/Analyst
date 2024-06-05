using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class LecturerEntity : EntityBase
    {
        public Guid Id { get; set; }
        public Guid TrainingFacilityId { get; set; }
        public Guid MajorId { get; set; }
        public string Username { get; set; } = null!;

        public virtual MajorEntity? Major { get; set; }
        public virtual TrainingFacilityEntity? TrainingFacility { get; set; }

        public virtual ICollection<ClassIndicatorEntity>? ClassIndicators { get; set; }
        public virtual ICollection<LecturerIndicatorEntity>? LecturerIndicators { get; set; }
    }
}
