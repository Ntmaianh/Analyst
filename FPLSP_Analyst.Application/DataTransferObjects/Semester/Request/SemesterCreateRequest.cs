using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Semester.Request
{
    public class SemesterCreateRequest
    {
        public Guid? GroupConfigId { get; set; }
        public string Code { get; set; } = null!;
        public EntityStatus Status { get; set; } = EntityStatus.InActive;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
