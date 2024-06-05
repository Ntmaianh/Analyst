using FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;


namespace FPLSP_Analyst.Infrastructure.ViewModels.ClassIndicator
{
    public class ClassIndicatorListWithPaginationViewModel : ViewModelBase<ViewClassIndicatorWithPaginationRequest>
    {
        private readonly IClassIndicatorReadOnlyRepository _classIndicatorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ClassIndicatorListWithPaginationViewModel(IClassIndicatorReadOnlyRepository classIndicatorReadOnlyRepository, ILocalizationService localizationService)
        {
            _classIndicatorReadOnlyRepository = classIndicatorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewClassIndicatorWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _classIndicatorReadOnlyRepository.GetClassIndicatorWithPaginationByAdminAsync(data, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the list of classIndicator"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of classIndicator")
                }
            };
            }
        }
    }
}
