using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.TraningFacility
{
    public class TrainingFacilityListWithPaginationViewModel : ViewModelBase<ViewTrainingFacilityWithPaginationRequest>
    {
        public readonly ITraningFacilityReadOnlyRepository _traningFacilityReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public TrainingFacilityListWithPaginationViewModel(ITraningFacilityReadOnlyRepository TraningFacilityReadOnlyRepository, ILocalizationService localizationService)
        {
            _traningFacilityReadOnlyRepository = TraningFacilityReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewTrainingFacilityWithPaginationRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _traningFacilityReadOnlyRepository.GetTrainingFacilityWithPaginationByAdminAsync(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of TrainingFactility"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of TrainingFactility")
                }
            };
            }
        }
    }
}
