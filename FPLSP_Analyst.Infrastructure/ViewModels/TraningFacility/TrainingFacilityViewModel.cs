using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.TraningFacility
{
    public class TrainingFacilityViewModel : ViewModelBase<Guid>
    {
        public readonly ITraningFacilityReadOnlyRepository _traningFacilityReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public TrainingFacilityViewModel(ITraningFacilityReadOnlyRepository TraningFacilityReadOnlyRepository, ILocalizationService localizationService)
        {
            _traningFacilityReadOnlyRepository = TraningFacilityReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idTraing, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _traningFacilityReadOnlyRepository.GetTraningFacilityByIdAsync(idTraing, cancellationToken);
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
                    Error = _localizationService["Error occurred while getting the TraingFactility"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "TraingFactility")
                }
            };
            }
        }
        public async Task HandleWithNameAsync(string name, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _traningFacilityReadOnlyRepository.GetTraningFacilityByNameAsync(name, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the TraingFactility"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "TraingFactility")
                }
            };
            }
        }
    }
}
