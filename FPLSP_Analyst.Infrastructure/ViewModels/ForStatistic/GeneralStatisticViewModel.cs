using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;


namespace FPLSP_Analyst.Infrastructure.ViewModels.ForStatistic
{
    public class GeneralStatisticViewModel : ViewModelBase<ViewGeneralStatisticWithPaginationRequest>
    {
        public readonly IGeneralStatisticReadOnlyResponsitory _generalStatisticReadOnlyResponsitory;
        private readonly ILocalizationService _localizationService;

        public GeneralStatisticViewModel(IGeneralStatisticReadOnlyResponsitory generalStatisticReadOnlyResponsitory, ILocalizationService localizationService)
        {
            _generalStatisticReadOnlyResponsitory = generalStatisticReadOnlyResponsitory;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewGeneralStatisticWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _generalStatisticReadOnlyResponsitory.GetGeneralStatisticWithPaginationByAdminAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Semester"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Semester")
                }
            };
            }
        }
    }
}
