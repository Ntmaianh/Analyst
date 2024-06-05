using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.ForSelect
{
    public class LecturerForSelectViewModel : ViewModelBase<LecturerForSelectRequest>
    {
        private readonly ILecturerReadOnlyRepository _lecturerReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public LecturerForSelectViewModel(ILecturerReadOnlyRepository lecturerReadOnlyRepository, ILocalizationService localizationService)
        {
            _lecturerReadOnlyRepository = lecturerReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(LecturerForSelectRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _lecturerReadOnlyRepository.GetLecturerForSelect(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of lecturer"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of lecturer")
                }
            };
            }
        }
    }
}
