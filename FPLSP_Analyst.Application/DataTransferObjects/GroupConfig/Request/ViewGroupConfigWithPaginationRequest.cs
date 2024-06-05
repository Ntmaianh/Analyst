using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request
{
    public class ViewGroupConfigWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
