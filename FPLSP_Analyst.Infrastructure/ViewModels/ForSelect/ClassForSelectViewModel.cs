using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.ForSelect
{
    public class ClassForSelectViewModel : ViewModelBase<ClassForSelectRequest>
    {
        private readonly IClassReadOnlyRepository _classReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public ClassForSelectViewModel(IClassReadOnlyRepository classReadOnlyRepository, ILocalizationService localizationService)
        {
            _classReadOnlyRepository = classReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(ClassForSelectRequest data, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _classReadOnlyRepository.GetClassForSelect(data, cancellationToken);

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
                    Error = _localizationService["Error occurred while getting the list of class"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of class")
                }
            };
            }
        }
    }
}
