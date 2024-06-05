using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Subject
{
    public class SubjectUpdateRangeViewModel : ViewModelBase<List<SubjectUpdateRequest>>
    {
        private readonly ISubjectReadWriteRepository _subjectReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SubjectUpdateRangeViewModel(ISubjectReadWriteRepository subjectReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _subjectReadWriteRepository = subjectReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<SubjectUpdateRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var upDateResult = await _subjectReadWriteRepository.UpdateRangeSubjectAsync(_mapper.Map<List<SubjectEntity>>(data), cancellationToken);
                Success = upDateResult.Success;
                ErrorItems = upDateResult.Errors;
                Message = upDateResult.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the Subject"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Subject")
                    }
                };
            }

        }
    }
}
