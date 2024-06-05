using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerDetailsViewModel : ViewModelBase<ViewLecturerDetailsWithPaginationRequest>
    {
        private readonly ILecturerReadOnlyRepository _lecturerDetailReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public LecturerDetailsViewModel(ILecturerReadOnlyRepository lecturerDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _lecturerDetailReadOnlyRepository = lecturerDetailReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewLecturerDetailsWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _lecturerDetailReadOnlyRepository.GetLecturerDetailsWithPaginationByAdminAsync(data, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the list of lecturerDetail"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of lecturerDetail")
                }
            };
            }
        }
    }
}
