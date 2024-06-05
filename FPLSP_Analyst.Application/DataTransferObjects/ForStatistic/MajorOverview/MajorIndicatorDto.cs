using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorOverview.Details;

namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorOverview
{
    public class MajorIndicatorDto
    {
        public string SemesterCode { get; set; }
        public string MajorCode { get; set; }

        public Guid SemesterId { get; set; }
        public List<StudentForMajorStatistic> studentForMajorStatistics { get; set; }
        public List<SubjectForMajorStatistic> subjectForMajorStatisics { get; set; }
        public List<ClassForMajorStatistic> classForMajorStatistics { get; set; }



    }
}
