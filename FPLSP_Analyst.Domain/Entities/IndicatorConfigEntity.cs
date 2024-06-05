using FPLSP_Analyst.Domain.Entities.Base;
using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Domain.Entities
{
    public class IndicatorConfigEntity : EntityBase
    {
        public Guid Id { get; set; }
        public EnumConfig.TableName Table { get; set; }
        public EnumConfig.ColumnName Column { get; set; }
        public string Predication { get; set; } = null!;
        public virtual ICollection<GroupIndicatorConfigEntity>? GroupIndicatorConfigs { get; set; }

    }
}
