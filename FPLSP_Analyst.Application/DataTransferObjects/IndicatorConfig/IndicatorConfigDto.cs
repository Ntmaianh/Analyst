using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig
{
    public class IndicatorConfigDto
    {
        public Guid Id { get; set; }
        public EnumConfig.TableName Table { get; set; }
        public EnumConfig.ColumnName Column { get; set; }
        public string Predication { get; set; }
    }
}
