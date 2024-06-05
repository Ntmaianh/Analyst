namespace FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request
{
    public class LectureCreateRequest
    {
        public Guid TrainingFacilityId { get; set; }
        public Guid MajorId { get; set; }
        public string Username { get; set; } = null!;
    }
}
