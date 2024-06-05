using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Semester
{
    public class SemesterViewModel : ViewModelBase<Guid>
    {
        public readonly ISemesterReadOnlyRepository _SemesterReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SemesterViewModel(ISemesterReadOnlyRepository SemesterReadOnlyRepository, ILocalizationService localizationService)
        {
            _SemesterReadOnlyRepository = SemesterReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idSemester, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _SemesterReadOnlyRepository.GetSemesterByIdAsync(idSemester, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Semester"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Semester")
                }
            };
            }
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _SemesterReadOnlyRepository.GetCurrentSemester(cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Semester"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Semester")
                }
            };
            }
        }
    }
}
