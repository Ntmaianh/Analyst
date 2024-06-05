using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request
{
    public class ViewLecturerIndicatorWithPaginationRequest : PaginationRequest
    {
        public string SearchString { get; set; } = null!;
        public string IndicatorField { get; set; } = null!;
        public float IndicatorValueMax { get; set; }
        public float IndicatorValueMin { get; set; }

        public Guid? SemesterId { get; set; }
        public Guid? MajorId { get; set; }
        public Guid? LecturerId { get; set; }
    }
}
