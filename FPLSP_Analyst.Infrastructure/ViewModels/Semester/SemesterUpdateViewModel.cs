using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Semester
{
    public class SemesterUpdateViewModel : ViewModelBase<SemesterUpdateRequest>
    {
        public readonly ISemesterReadWriteRepository _SemesterReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SemesterUpdateViewModel(ISemesterReadWriteRepository SemesterReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _SemesterReadWriteRepository = SemesterReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(SemesterUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _SemesterReadWriteRepository.UpdateSemesterAsync(_mapper.Map<SemesterEntity>(request), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Semester")
                    }
                };
            }
        }

        public async Task HandleStatusChangedAsync(Guid idActiveSemester, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _SemesterReadWriteRepository.UpdateRangeStatusOfSemesterAsync(idActiveSemester, cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Semester")
                    }
                };
            }
        }
    }
}
