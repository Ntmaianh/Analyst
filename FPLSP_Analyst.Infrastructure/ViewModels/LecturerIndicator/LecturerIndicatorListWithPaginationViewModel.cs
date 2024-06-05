using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;


namespace FPLSP_Analyst.Infrastructure.ViewModels.LecturerIndicator
{
    public class LecturerIndicatorListWithPaginationViewModel : ViewModelBase<ViewLecturerIndicatorWithPaginationRequest>
    {
        private readonly ILecturerIndicatorReadOnlyRepository _lecturerIndicatorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public LecturerIndicatorListWithPaginationViewModel(ILecturerIndicatorReadOnlyRepository lecturerIndicatorReadOnlyRepository, ILocalizationService localizationService)
        {
            _lecturerIndicatorReadOnlyRepository = lecturerIndicatorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewLecturerIndicatorWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var lectureIndicator = await _lecturerIndicatorReadOnlyRepository.GetLecturerIndicatorWithPaginationByAdminAsync(data, cancellationToken);
                Data = lectureIndicator.Data!;
                Success = lectureIndicator.Success;
                ErrorItems = lectureIndicator.Errors;
                Message = lectureIndicator.Message;
            }
            catch (Exception)
            {

                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the list of lecturerIndicator"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of lecturerIndicator")
                }
            };
            }
        }
    }
}
