namespace FPLSP_Analyst.Application.DataTransferObjects.Major.Request
{
    public class MajorUpdateRangeRequest
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
    }
}
