using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Class
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid LecturerId { get; set; }
        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTimeOffset CreatedTime { get; set; }
    }
}
