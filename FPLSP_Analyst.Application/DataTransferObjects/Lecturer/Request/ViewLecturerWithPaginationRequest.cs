using FPLSP_Analyst.Application.ValueObjects.Pagination;

namespace FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request
{
    public class ViewLecturerWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
        public Guid? TrainingFacilityId { get; set; }
        public Guid? MajorId { get; set; }
    }
}
