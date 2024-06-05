using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.ForSelect
{
    public class MajorForSelectViewModel : ViewModelBase<MajorForSelectRequest>
    {
        private readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public MajorForSelectViewModel(IMajorReadOnlyRepository majorReadOnlyRepository, ILocalizationService localizationService)
        {
            _majorReadOnlyRepository = majorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(MajorForSelectRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadOnlyRepository.GetMajorForSelect(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of major"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of major")
                }
            };
            }
        }
    }
}
