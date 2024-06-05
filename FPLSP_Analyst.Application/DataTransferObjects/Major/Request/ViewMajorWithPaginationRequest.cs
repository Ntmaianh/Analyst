using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Major.Request
{
    public class ViewMajorWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
        public Guid? SemesterId { get; set; }
    }
}
