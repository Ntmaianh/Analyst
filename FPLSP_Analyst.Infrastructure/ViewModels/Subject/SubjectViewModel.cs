using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Subject
{
    public class SubjectViewModel : ViewModelBase<Guid>
    {
        private readonly ISubjectReadOnlyRepository _subjectReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SubjectViewModel(ISubjectReadOnlyRepository SubjectReadOnlyRepository, ILocalizationService localizationService)
        {
            _subjectReadOnlyRepository = SubjectReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idSubject, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectReadOnlyRepository.GetSubjectByIdAsync(idSubject, cancellationToken);

                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch
            {
                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the Subject"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Subject")
                }
            };
            }
        }
    }
}
