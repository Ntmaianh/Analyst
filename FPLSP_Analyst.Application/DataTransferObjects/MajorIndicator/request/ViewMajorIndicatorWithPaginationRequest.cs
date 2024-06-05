using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator.request
{
    public class ViewMajorIndicatorWithPaginationRequest : PaginationRequest
    {
        public string SearchString { get; set; } = null!;
        public string IndicatorField { get; set; } = null!;

        public float IndicatorValueMax { get; set; }
        public float IndicatorValueMin { get; set; }
        public Guid? SemesterId { get; set; }
    }
}
