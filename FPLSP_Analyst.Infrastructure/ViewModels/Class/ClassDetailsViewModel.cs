using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Class
{
    public class ClassDetailsViewModel : ViewModelBase<Guid>
    {
        private readonly IClassReadOnlyRepository _classDetailReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ClassDetailsViewModel(IClassReadOnlyRepository classDetailReadOnlyRepository, ILocalizationService localizationService)
        {
            _classDetailReadOnlyRepository = classDetailReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(Guid idClass, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _classDetailReadOnlyRepository.GetClassDetailsAsync(idClass, cancellationToken);
                Data = result.Data!;
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
                    Error = _localizationService["Error occurred while getting the list of classDetail"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of classDetail")
                }
            };
            }
        }
    }
}
