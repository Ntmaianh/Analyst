using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Subject
{
    public class SubjectDeleteViewModel : ViewModelBase<SubjectDeleteRequest>
    {
        private readonly ISubjectReadWriteRepository _subjectReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SubjectDeleteViewModel(ISubjectReadWriteRepository SubjectReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _subjectReadWriteRepository = SubjectReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(SubjectDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectReadWriteRepository.DeleteSubjectAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the subject"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "subject")
                    }
                };
            }
        }
    }
}
