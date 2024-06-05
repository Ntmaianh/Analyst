using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.TraningFacility
{
    public class TrainingFacilityUpdateViewModel : ViewModelBase<TrainingFacilityUpdateRequest>
    {
        private readonly ITraningFacilityReadWriteRepository _traningFacilityReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TrainingFacilityUpdateViewModel(ITraningFacilityReadWriteRepository TraningFacilityReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _traningFacilityReadWriteRepository = TraningFacilityReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(TrainingFacilityUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _traningFacilityReadWriteRepository.UpdateTrainingFacilityAsync(_mapper.Map<TrainingFacilityEntity>(request), cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the trainingFacility"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "trainingFacility")
                    }
                };
            }
        }
    }
}
