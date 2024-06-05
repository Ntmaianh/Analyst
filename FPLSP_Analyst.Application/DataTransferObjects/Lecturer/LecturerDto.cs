using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Lecturer
{
    public class LecturerDto
    {
        public Guid Id { get; set; }
        public Guid TrainingFacilityId { get; set; }
        public Guid MajorId { get; set; }
        public string MajorCode { get; set; } = null!;
        public string Username { get; set; } = null!;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public DateTimeOffset CreatedTime { get; set; }
    }
}
