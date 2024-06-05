namespace FPLSP_Analyst.Application.DataTransferObjects.Semester.Request
{
    public class SemesterUpdateRequest
    {
        public Guid Id { get; set; }
        public Guid GroupConfigId { get; set; }
        public string Code { get; set; } = null!;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
