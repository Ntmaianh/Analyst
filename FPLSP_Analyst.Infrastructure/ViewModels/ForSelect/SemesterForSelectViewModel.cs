using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.ForSelect
{
    public class SemesterForSelectViewModel : ViewModelBase<SemesterForSelectRequest>
    {
        private readonly ISemesterReadOnlyRepository _semesterReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SemesterForSelectViewModel(ISemesterReadOnlyRepository semesterReadOnlyRepository, ILocalizationService localizationService)
        {
            _semesterReadOnlyRepository = semesterReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(SemesterForSelectRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _semesterReadOnlyRepository.GetSemesterForSelect(data, cancellationToken);

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
