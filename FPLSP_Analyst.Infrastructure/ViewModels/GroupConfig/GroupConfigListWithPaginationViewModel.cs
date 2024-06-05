using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.GroupConfig
{
    public class GroupConfigListWithPaginationViewModel : ViewModelBase<ViewGroupConfigWithPaginationRequest>
    {
        public readonly IGroupConfigReadOnlyRepository _GroupConfigReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public GroupConfigListWithPaginationViewModel(IGroupConfigReadOnlyRepository GroupConfigReadOnlyRepository, ILocalizationService localizationService)
        {
            _GroupConfigReadOnlyRepository = GroupConfigReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ViewGroupConfigWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _GroupConfigReadOnlyRepository.GetGroupConfigWithPaginationByAdminAsync(request, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of GroupConfig"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of GroupConfig")
                }
            };
            }
        }
    }
}
