using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.IndicatorConfig
{
    public class GroupConfigUpdateViewModel : ViewModelBase<GroupConfigUpdateRequest>
    {
        public readonly IIndicatorConfigReadWriteRepository _IndicatorConfigReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public GroupConfigUpdateViewModel(IIndicatorConfigReadWriteRepository IndicatorConfigReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _IndicatorConfigReadWriteRepository = IndicatorConfigReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(GroupConfigUpdateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _IndicatorConfigReadWriteRepository.UpdateIndicatorConfigAsync(_mapper.Map<IndicatorConfigEntity>(data), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "IndicatorConfig")
                    }
                };
            }
        }
    }
}
