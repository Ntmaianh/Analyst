namespace FPLSP_Analyst.Application.DataTransferObjects.Subject.Request
{
    public class SubjectUpdateRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
