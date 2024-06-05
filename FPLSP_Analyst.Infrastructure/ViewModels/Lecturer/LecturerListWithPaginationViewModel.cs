using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerListWithPaginationViewModel : ViewModelBase<ViewLecturerWithPaginationRequest>
    {
        public readonly ILecturerReadOnlyRepository _lectureReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public LecturerListWithPaginationViewModel(ILecturerReadOnlyRepository LectureReadOnlyRepository, ILocalizationService localizationService)
        {
            _lectureReadOnlyRepository = LectureReadOnlyRepository;
            _localizationService = localizationService;
        }

        public override async Task HandleAsync(ViewLecturerWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _lectureReadOnlyRepository.GetLecturerWithPaginationByAdminAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of Lecture"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of Lecture")
                }
            };
            }
        }
    }
}
