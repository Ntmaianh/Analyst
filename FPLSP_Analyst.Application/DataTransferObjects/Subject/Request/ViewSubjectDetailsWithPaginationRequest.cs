using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Subject.Request
{
    public class ViewSubjectDetailsWithPaginationRequest : PaginationRequest
    {
        public Guid SubjectId { get; set; }
    }
}
