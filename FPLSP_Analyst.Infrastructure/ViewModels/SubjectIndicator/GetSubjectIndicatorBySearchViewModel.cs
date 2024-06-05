using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.SubjectIndicator
{
    public class GetSubjectIndicatorBySearchViewModel : ViewModelBase<SubjectIndicatorRequest>
    {
        private readonly ISubjectIndicatorReadOnlyRepository _subjectIndicatorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GetSubjectIndicatorBySearchViewModel(ISubjectIndicatorReadOnlyRepository SubjectIndicatorReadOnlyRepository, ILocalizationService localizationService)
        {
            _subjectIndicatorReadOnlyRepository = SubjectIndicatorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(SubjectIndicatorRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectIndicatorReadOnlyRepository.GetSubjectIndicatorBySearchAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the subjectIndicator"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "subjectIndicator")
                }
            };
            }
        }
    }
}
