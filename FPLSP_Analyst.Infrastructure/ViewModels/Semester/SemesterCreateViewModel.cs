using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Semester
{
    public class SemesterCreateViewModel : ViewModelBase<SemesterCreateRequest>
    {
        public readonly ISemesterReadWriteRepository _SemesterReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SemesterCreateViewModel(ISemesterReadWriteRepository SemesterReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _SemesterReadWriteRepository = SemesterReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(SemesterCreateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _SemesterReadWriteRepository.AddSemesterAsync(_mapper.Map<SemesterEntity>(data), cancellationToken);

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
                        Error = _localizationService["Error occurred while creating the Semester"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Semester")
                    }
                };
            }
        }
    }
}
