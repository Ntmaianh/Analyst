using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.IndicatorConfig
{
    public class IndicatorConfigListWithPaginationViewModel : ViewModelBase<ViewIndicatorConfigWithPaginationRequest>
    {
        public readonly IIndicatorConfigReadOnlyRepository _IndicatorConfigReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public IndicatorConfigListWithPaginationViewModel(IIndicatorConfigReadOnlyRepository IndicatorConfigReadOnlyRepository, ILocalizationService localizationService)
        {
            _IndicatorConfigReadOnlyRepository = IndicatorConfigReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewIndicatorConfigWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _IndicatorConfigReadOnlyRepository.GetIndicatorConfigWithPaginationByAdminAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of IndicatorConfig"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of IndicatorConfig")
                }
            };
            }
        }
    }
}
