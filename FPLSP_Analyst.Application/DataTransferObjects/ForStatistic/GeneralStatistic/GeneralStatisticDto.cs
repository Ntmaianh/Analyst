namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic
{
    public class GeneralStatisticDto
    {

        public Guid SemesterId { get; set; }
        public string SemesterCode { get; set; } = null!;

        public int MajorTotal { get; set; }

        public int SubjectTotal { get; set; }

        public int ClassTotal { get; set; }

        public int LecturerTotal { get; set; }
    }
}
