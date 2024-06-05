using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.IndicatorConfig
{
    public class IndicatorConfigCreateViewModel : ViewModelBase<IndicatorConfigCreateRequest>
    {
        public readonly IIndicatorConfigReadOnlyRepository _IndicatorConfigReadOnlyRepository;
        public readonly IIndicatorConfigReadWriteRepository _IndicatorConfigReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public IndicatorConfigCreateViewModel(IIndicatorConfigReadOnlyRepository IndicatorConfigReadOnlyRepository, IIndicatorConfigReadWriteRepository IndicatorConfigReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _IndicatorConfigReadOnlyRepository = IndicatorConfigReadOnlyRepository;
            _IndicatorConfigReadWriteRepository = IndicatorConfigReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(IndicatorConfigCreateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _IndicatorConfigReadWriteRepository.AddIndicatorConfigAsync(_mapper.Map<IndicatorConfigEntity>(Data), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _IndicatorConfigReadOnlyRepository.GetIndicatorConfigByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the IndicatorConfig"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "IndicatorConfig")
                    }
                };
            }
        }
    }
}
