namespace FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator.Request
{
    public class ClassIndicatiorCreateRequest
    {
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid LecturerId { get; set; }
    }
}
