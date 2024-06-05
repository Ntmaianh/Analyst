using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.ViewModels.Excels
{
    public class ExcelSubjectVM
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string TrainingType { get; set; }
        public string MajorName { get; set; }
        public SubjectEntity ConvertToEntity()
        {
            return new SubjectEntity()
            {
                Code = Code,
                Name = Name,
                TrainingType = TrainingType
            };
        }
    }
}
