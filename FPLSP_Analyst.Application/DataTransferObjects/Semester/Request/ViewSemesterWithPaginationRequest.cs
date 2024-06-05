using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Semester.Request
{
    public class ViewSemesterWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
        public Guid? GroupConfigId { get; set; }
    }
}
