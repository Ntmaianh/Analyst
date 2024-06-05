using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Semester
{
    public class SemesterListWithPaginationViewModel : ViewModelBase<ViewSemesterWithPaginationRequest>
    {
        public readonly ISemesterReadOnlyRepository _SemesterReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SemesterListWithPaginationViewModel(ISemesterReadOnlyRepository SemesterReadOnlyRepository, ILocalizationService localizationService)
        {
            _SemesterReadOnlyRepository = SemesterReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewSemesterWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _SemesterReadOnlyRepository.GetSemesterWithPaginationByAdminAsync(data, cancellationToken);

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
