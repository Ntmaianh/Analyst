using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Major.Request
{
    public class ViewMajorDetailsWithPaginationRequest : PaginationRequest
    {
        public Guid MajorId { get; set; }
    }
}
