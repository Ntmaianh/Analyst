using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.TraningFacility
{
    public class TrainingFacilityDeleteRangeViewModel : ViewModelBase<List<TrainingFacilityDeleteRequest>>
    {
        private readonly ITraningFacilityReadWriteRepository _TrainingFacilityReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TrainingFacilityDeleteRangeViewModel(ITraningFacilityReadWriteRepository TrainingFacilityReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _TrainingFacilityReadWriteRepository = TrainingFacilityReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<TrainingFacilityDeleteRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _TrainingFacilityReadWriteRepository.RemoveRangeTraningFacilityAsync(data, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the TrainingFacility"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "TrainingFacility")
                    }
                };
            }
        }
    }
}
