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
    public class LecturerCreateRangeViewModel : ViewModelBase<List<LectureCreateRequest>>
    {
        private readonly ILecturerReadOnlyRepository _lectureReadOnlyRepository;
        private readonly ILecturerReadWriteRepository _lectureReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerCreateRangeViewModel(ILecturerReadOnlyRepository LectureReadOnlyRepository, ILecturerReadWriteRepository LectureReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _lectureReadOnlyRepository = LectureReadOnlyRepository;
            _lectureReadWriteRepository = LectureReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<LectureCreateRequest> listData, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _lectureReadWriteRepository.CreateRangeLecturerAsync(_mapper.Map<List<LecturerEntity>>(listData), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _lectureReadOnlyRepository.GetListLecturerByIdAsync(createResult.Data, cancellationToken);
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
