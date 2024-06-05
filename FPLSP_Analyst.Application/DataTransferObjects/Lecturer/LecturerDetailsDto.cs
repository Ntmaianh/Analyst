using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Lecturer
{
    public class LecturerDetailsDto
    {
        public Guid Id { get; set; }
        public Guid TrainingFacilityId { get; set; }
        public Guid MajorId { get; set; }
        public string Username { get; set; } = null!;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public PaginationResponse<LecturerIndicatorForDetails> LecturerIndicatorForDetails { get; set; }
    }
}
