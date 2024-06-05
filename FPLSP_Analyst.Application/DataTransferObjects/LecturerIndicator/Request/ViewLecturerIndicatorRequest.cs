namespace FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request
{
    public class ViewLecturerIndicatorRequest
    {
        public Guid? SemesterId { get; set; }
        public Guid? MajorId { get; set; }
        public Guid? LecturerId { get; set; }
    }
}
