using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.GroupConfig
{
    public class GroupConfigCreateViewModel : ViewModelBase<GroupConfigCreateRequest>
    {

        public readonly IGroupConfigReadOnlyRepository _GroupConfigReadOnlyRepository;
        public readonly IGroupConfigReadWriteRepository _GroupConfigReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public GroupConfigCreateViewModel(IGroupConfigReadOnlyRepository GroupConfigReadOnlyRepository, IGroupConfigReadWriteRepository GroupConfigReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _GroupConfigReadOnlyRepository = GroupConfigReadOnlyRepository;
            _GroupConfigReadWriteRepository = GroupConfigReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(GroupConfigCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _GroupConfigReadWriteRepository.AddGroupConfigAsync(_mapper.Map<GroupConfigEntity>(request), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _GroupConfigReadOnlyRepository.GetGroupConfigByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the GroupConfig"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "GroupConfig")
                    }
                };
            }
        }
    }
}
