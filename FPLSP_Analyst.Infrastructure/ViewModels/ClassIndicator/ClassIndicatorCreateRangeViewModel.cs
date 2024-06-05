using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.ClassIndicator
{
    public class ClassIndicatorCreateRangeViewModel : ViewModelBase<List<ClassIndicatiorCreateRequest>>
    {
        private readonly IClassIndicatorReadWriteRepository _ClassIndicatorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public ClassIndicatorCreateRangeViewModel(IClassIndicatorReadWriteRepository ClassIndicatorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _ClassIndicatorReadWriteRepository = ClassIndicatorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<ClassIndicatiorCreateRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _ClassIndicatorReadWriteRepository.CreateRangeClassIndicatorAsync(_mapper.Map<List<ClassIndicatorEntity>>(data), cancellationToken);
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
            }
            catch (Exception)
            {

                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the ClassIndicator"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "ClassIndicator")
                    }
                };
            }
        }
    }
}
