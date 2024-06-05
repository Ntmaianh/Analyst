using FPLSP_Analyst.Domain.Enums;
namespace FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request
{
    public class IndicatorConfigCreateRequest
    {
        public EnumConfig.TableName Table { get; set; }
        public EnumConfig.ColumnName Column { get; set; }
        public string Predication { get; set; } 
    }
}
