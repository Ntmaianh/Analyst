using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Subject.Request
{
    public class ViewSubjectWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }

        public Guid? MajorId { get; set; }
    }
}
