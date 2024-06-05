using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerCreateViewModel : ViewModelBase<LectureCreateRequest>
    {
        public readonly ILecturerReadOnlyRepository _lectureReadOnlyRepository;
        public readonly ILecturerReadWriteRepository _lectureReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerCreateViewModel(ILecturerReadOnlyRepository lectureReadOnlyRepository, ILecturerReadWriteRepository lectureReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _lectureReadOnlyRepository = lectureReadOnlyRepository;
            _lectureReadWriteRepository = lectureReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public override async Task HandleAsync(LectureCreateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _lectureReadWriteRepository.AddLecturerAsync(_mapper.Map<LecturerEntity>(data), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _lectureReadOnlyRepository.GetLecturerByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Lecture"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Lecture")
                    }
                };
            }
        }
    }
}
