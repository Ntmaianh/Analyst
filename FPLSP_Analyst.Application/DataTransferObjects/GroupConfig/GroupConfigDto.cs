namespace FPLSP_Analyst.Application.DataTransferObjects.GroupConfig
{
    public class GroupConfigDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public int NumberOfSemester { get; set; }
    }
}
