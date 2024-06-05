namespace FPLSP_Analyst.Application.DataTransferObjects.Major.Request
{
    public class MajorUpdateRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
    }
}
