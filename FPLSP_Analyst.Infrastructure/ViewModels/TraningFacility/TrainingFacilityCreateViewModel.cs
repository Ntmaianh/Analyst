using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.TraningFacility
{
    public class TrainingFacilityCreateViewModel : ViewModelBase<TrainingFacilityCreateRequest>
    {
        private readonly ITraningFacilityReadWriteRepository _traningFacilityReadWriteRepository;
        private readonly ITraningFacilityReadOnlyRepository _traningFacilityReadOnlyRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public TrainingFacilityCreateViewModel(ITraningFacilityReadWriteRepository TraningFacilityReadWriteRepository, ITraningFacilityReadOnlyRepository traningFacilityReadOnlyRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _traningFacilityReadWriteRepository = TraningFacilityReadWriteRepository;
            _traningFacilityReadOnlyRepository = traningFacilityReadOnlyRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(TrainingFacilityCreateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _traningFacilityReadWriteRepository.AddTrainingFacilityAsync(_mapper.Map<TrainingFacilityEntity>(data), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _traningFacilityReadOnlyRepository.GetTraningFacilityByIdAsync(createResult.Data, cancellationToken);

                    Data = result.Data!;
                    Success = result.Success;
                    ErrorItems = result.Errors;
                    Message = result.Message;
                    return;
                }

                Success = createResult.Success;
                ErrorItems = createResult.Errors;
                Message = createResult.Message;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the TrainingFacility"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "TrainingFacility")
                    }
                };
            }
        }
    }
}
