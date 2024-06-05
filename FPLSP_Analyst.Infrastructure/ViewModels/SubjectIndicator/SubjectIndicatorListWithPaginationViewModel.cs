using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;


namespace FPLSP_Analyst.Infrastructure.ViewModels.SubjectIndicator
{
    public class SubjectIndicatorListWithPaginationViewModel : ViewModelBase<ViewSubjectIndicatorWithPaginationRequest>
    {
        private readonly ISubjectIndicatorReadOnlyRepository _subjectIndicatorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SubjectIndicatorListWithPaginationViewModel(ISubjectIndicatorReadOnlyRepository subjectIndicatorReadOnlyRepository, ILocalizationService localizationService)
        {
            _subjectIndicatorReadOnlyRepository = subjectIndicatorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewSubjectIndicatorWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectIndicatorReadOnlyRepository.GetSubjectIndicatorWithPaginationByAdminAsync(data, cancellationToken);
                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
            }
            catch (Exception)
            {

                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the list of subjectIndicator"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of subjectIndicator")
                }
            };
            }
        }
    }
}
