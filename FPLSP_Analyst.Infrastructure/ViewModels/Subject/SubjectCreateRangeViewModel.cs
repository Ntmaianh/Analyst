using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Subject
{
    public class SubjectCreateRangeViewModel : ViewModelBase<List<SubjectCreateRequest>>
    {
        private readonly ISubjectReadOnlyRepository _subjectReadOnlyRepository;
        private readonly ISubjectReadWriteRepository _subjectReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SubjectCreateRangeViewModel(ISubjectReadOnlyRepository SubjectReadOnlyRepository, ISubjectReadWriteRepository SubjectReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _subjectReadOnlyRepository = SubjectReadOnlyRepository;
            _subjectReadWriteRepository = SubjectReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<SubjectCreateRequest> listData, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _subjectReadWriteRepository.CreateRangeSubjectAsync(_mapper.Map<List<SubjectEntity>>(listData), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _subjectReadOnlyRepository.GetListSubjectByIdAsync(createResult.Data, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the subject"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "subject")
                    }
                };
            }

        }
    }
}
