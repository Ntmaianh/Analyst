using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.GroupConfig
{
    public class GroupConfigViewModel : ViewModelBase<Guid>
    {
        public readonly IGroupConfigReadOnlyRepository _GroupConfigReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GroupConfigViewModel(IGroupConfigReadOnlyRepository GroupConfigReadOnlyRepository, ILocalizationService localizationService)
        {
            _GroupConfigReadOnlyRepository = GroupConfigReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idGroupConfig, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _GroupConfigReadOnlyRepository.GetGroupConfigByIdAsync(idGroupConfig, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the GroupConfig"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "GroupConfig")
                }
            };
            }
        }
    }
}
