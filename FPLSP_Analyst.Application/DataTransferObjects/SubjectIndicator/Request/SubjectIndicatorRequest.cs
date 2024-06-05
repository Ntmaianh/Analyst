namespace FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator.Request
{
    public class SubjectIndicatorRequest
    {
        public Guid? SemesterId { get; set; }
        public Guid? MajorId { get; set; }
        public Guid? SubjectId { get; set; }
    }
}
