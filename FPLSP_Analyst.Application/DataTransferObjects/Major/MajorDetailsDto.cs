using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Major
{
    public class MajorDetailsDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Major { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public PaginationResponse<MajorIndicatorForDetails> MajorIndicatorForDetails { get; set; }
    }
}
