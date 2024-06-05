using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorViewModel : ViewModelBase<Guid>
    {
        private readonly IMajorReadOnlyRepository _mMajorReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public MajorViewModel(IMajorReadOnlyRepository MajorReadOnlyRepository, ILocalizationService localizationService)
        {
            _mMajorReadOnlyRepository = MajorReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mMajorReadOnlyRepository.GetMajorByIdAsync(idMajor, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the Major"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Major")
                }
            };
            }
        }
    }
}
