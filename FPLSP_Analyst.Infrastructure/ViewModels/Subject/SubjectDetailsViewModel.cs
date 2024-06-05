using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Subject
{
    public class SubjectDetailsViewModel : ViewModelBase<ViewSubjectDetailsWithPaginationRequest>
    {
        private readonly ISubjectReadOnlyRepository _subjectReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SubjectDetailsViewModel(ISubjectReadOnlyRepository subjectDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _subjectReadOnlyRepository = subjectDetailReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewSubjectDetailsWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectReadOnlyRepository.GetSubjectDetailsWithPaginationByAdminAsync(data, cancellationToken);
                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the list of majorDetail"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of majorDetail")
                }
            };
            }
        }
    }
}
