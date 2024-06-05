using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.IndicatorConfig
{
    public class IndicatorConfigDeleteViewModel : ViewModelBase<IndicatorConfigDeleteRequest>
    {
        public readonly IIndicatorConfigReadWriteRepository _IndicatorConfigReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public IndicatorConfigDeleteViewModel(IIndicatorConfigReadWriteRepository IndicatorConfigReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _IndicatorConfigReadWriteRepository = IndicatorConfigReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(IndicatorConfigDeleteRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _IndicatorConfigReadWriteRepository.DeleteIndicatorConfigAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the IndicatorConfig"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "IndicatorConfig")
                    }
                };
            }
        }
    }
}
