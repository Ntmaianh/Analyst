using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.LecturerIndicator
{
    public class GetLecturerIndicatorWithPaginationBySearchViewModel : ViewModelBase<ViewLecturerIndicatorRequest>
    {
        private readonly ILecturerIndicatorReadOnlyRepository _lecturerIndicatorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GetLecturerIndicatorWithPaginationBySearchViewModel(ILecturerIndicatorReadOnlyRepository LecturerIndicatorReadOnlyRepository, ILocalizationService localizationService)
        {
            _lecturerIndicatorReadOnlyRepository = LecturerIndicatorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public async override Task HandleAsync(ViewLecturerIndicatorRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _lecturerIndicatorReadOnlyRepository.GetLecturerIndicatorWithPaginationBySearchAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the lecturer"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "lecturer")
                }
            };
            }
        }
    }
}
