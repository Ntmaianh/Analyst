namespace FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request
{
    public class LecturerIndicatorCreateRequest
    {
        public Guid SemesterId { get; set; }
        public Guid LecturerId { get; set; }
    }
}
