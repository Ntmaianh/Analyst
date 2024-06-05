using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request
{
    public class ViewLecturerDetailsWithPaginationRequest : PaginationRequest
    {
        public Guid LecturerId { get; set; }
        public string UserEmail { get; set; }
    }
}
