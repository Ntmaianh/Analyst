using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Application.DataTransferObjects.Class
{
    public class ClassDetailsDto
    {
        public Guid Id { get; set; }

        public EntityStatus Status { get; set; } = EntityStatus.Active;
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid LecturerId { get; set; }
        public Guid MajorId { get; set; }
        public string SubjectCode { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string LecturerName { get; set; } = null!;
        public string MajorName { get; set; } = null!;
        public string SemesterCode { get; set; } = null!;

        public int StudentTotalNumber { get; set; }
        public int StudentPassedNumber { get; set; }
        public int StudentFailedNumber { get; set; }
        public int StudentBannedNumber { get; set; }

        public float StudentPassedPercent { get; set; }
        public float StudentFailedPercent { get; set; }
        public float StudentBannedPercent { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset ModifiedTime { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
        public string Semester { get; set; } = null!;
    }
}
