using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;


namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorDetailsViewModel : ViewModelBase<ViewMajorDetailsWithPaginationRequest>
    {
        private readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public MajorDetailsViewModel(IMajorReadOnlyRepository majorReadOnlyRepository, ILocalizationService localizationService)
        {
            _majorReadOnlyRepository = majorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewMajorDetailsWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadOnlyRepository.GetMajorDetailsWithPaginationByAdminAsync(data, cancellationToken);
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
