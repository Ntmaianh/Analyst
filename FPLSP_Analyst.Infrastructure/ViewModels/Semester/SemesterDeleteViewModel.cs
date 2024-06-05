using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Semester
{
    public class SemesterDeleteViewModel : ViewModelBase<SemesterDeleteRequest>
    {
        public readonly ISemesterReadWriteRepository _SemesterReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SemesterDeleteViewModel(ISemesterReadWriteRepository SemesterReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _SemesterReadWriteRepository = SemesterReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(SemesterDeleteRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _SemesterReadWriteRepository.DeleteSemesterAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Semester"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Semester")
                    }
                };
            }
        }
    }
}
