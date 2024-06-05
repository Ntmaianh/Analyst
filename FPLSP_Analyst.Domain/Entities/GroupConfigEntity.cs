using FPLSP_Analyst.Domain.Entities.Base;

namespace FPLSP_Analyst.Domain.Entities
{
    public class GroupConfigEntity : EntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;

        public virtual ICollection<SemesterEntity>? Semesters { get; set; }
        public virtual ICollection<GroupIndicatorConfigEntity>? GroupIndicatorConfigs { get; set; }

    }
}
