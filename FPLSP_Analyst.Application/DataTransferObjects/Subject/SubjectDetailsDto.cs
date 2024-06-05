using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Subject
{
    public class SubjectDetailsDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public EntityStatus Status { get; set; }
        public PaginationResponse<SubjectIndicatorForDetails> SubjectIndicatorForDetails { get; set; }
    }
}
