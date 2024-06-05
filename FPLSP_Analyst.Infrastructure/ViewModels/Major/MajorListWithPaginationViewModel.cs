using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorListWithPaginationViewModel : ViewModelBase<ViewMajorWithPaginationRequest>
    {
        private readonly IMajorReadOnlyRepository _mMajorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public MajorListWithPaginationViewModel(IMajorReadOnlyRepository MajorReadOnlyRepository, ILocalizationService localizationService)
        {
            _mMajorReadOnlyRepository = MajorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewMajorWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mMajorReadOnlyRepository.GetMajorWithPaginationByAdminAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Major"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Major")
                }
            };
            }
        }
    }
}
