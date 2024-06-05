using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator.request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.MajorIndicator
{
    public class GetMajorIndicatorWithPaginationBySearchViewModel : ViewModelBase<MajorIndicatorWithPaginationBySearchRequest>
    {
        private readonly IMajorIndicatorReadOnlyRepository _majorIndicatorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GetMajorIndicatorWithPaginationBySearchViewModel(IMajorIndicatorReadOnlyRepository majorIndicatorReadOnlyRepository, ILocalizationService localizationService)
        {
            _majorIndicatorReadOnlyRepository = majorIndicatorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(MajorIndicatorWithPaginationBySearchRequest data, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _majorIndicatorReadOnlyRepository.GetMajorIndicatorWithPaginationBySearchAsync(data, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the list of majorIndicator"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of majorIndicator")
                }
            };
            }
        }
    }

}
