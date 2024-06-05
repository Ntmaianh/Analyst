using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.SubjectIndicator
{
    public class SubjectIndicatorCreateRangeViewModel : ViewModelBase<List<SubjectIndicatiorCreateRequest>>
    {
        private readonly ISubjectIndicatorReadWriteRepository _subjectIndicatorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SubjectIndicatorCreateRangeViewModel(ISubjectIndicatorReadWriteRepository subjectIndicatorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _subjectIndicatorReadWriteRepository = subjectIndicatorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<SubjectIndicatiorCreateRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectIndicatorReadWriteRepository.CreateRangeSubjectIndicatorAsync(_mapper.Map<List<SubjectIndicatorEntity>>(data), cancellationToken);
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
            }
            catch (Exception)
            {

                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while getting the subjectIndicator"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "subjectIndicator")
                    }
                };
            }

        }
    }
}
