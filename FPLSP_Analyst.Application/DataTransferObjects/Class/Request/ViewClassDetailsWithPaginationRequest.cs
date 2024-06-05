using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Class.Request
{
    public class ViewClassDetailsWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
