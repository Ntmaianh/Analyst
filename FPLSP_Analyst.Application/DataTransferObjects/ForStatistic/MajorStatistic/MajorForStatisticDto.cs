using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorStatistic.Details;

namespace FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorStatistic
{
    public class MajorForStatisticDto
    {
        public string Code { get; set; }
        public List<SubjectIndicatorForMajorStatistic> subjectIndicatorDtos { get; set; }
        public List<ClassIndicatorForMajorStatistic> classIndicatorDtos { get; set; }
        public List<LecturerIndicatorForMajorStatistic> lecturerIndicatorDtos { get; set; }
    }
}
