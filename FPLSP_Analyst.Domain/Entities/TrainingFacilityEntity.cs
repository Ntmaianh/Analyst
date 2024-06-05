using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class TrainingFacilityEntity : EntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<LecturerEntity>? Lecturers { get; set; }
    }
}
