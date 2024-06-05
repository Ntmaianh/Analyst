namespace FPLSP_Analyst.Application.DataTransferObjects.Subject
{
    public class SubjectDto
    {
        public Guid Id { get; set; }
        public Guid MajorId { get; set; }
        public string MajorCode { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

    }
}
