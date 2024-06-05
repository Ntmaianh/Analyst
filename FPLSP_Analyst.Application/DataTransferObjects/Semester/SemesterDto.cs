using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Semester
{
    public class SemesterDto
    {
        public Guid Id { get; set; }
        public Guid GroupConfigId { get; set; }
        public string GroupConfigCode { get; set; } = null!;
        public string Code { get; set; } = null!;
        public EntityStatus Status { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
