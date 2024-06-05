namespace FPLSP_Analyst.Application.DataTransferObjects.Major
{
    public class MajorDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public int NumberOfSubject { get; set; }
        public int NumberOfLecturer { get; set; }
    }
}
