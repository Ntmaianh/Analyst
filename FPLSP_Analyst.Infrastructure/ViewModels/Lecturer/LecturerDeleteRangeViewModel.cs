using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerDeleteRangeViewModel : ViewModelBase<List<LectureDeleteRequest>>
    {
        private readonly ILecturerReadWriteRepository _lectureReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerDeleteRangeViewModel(ILecturerReadWriteRepository LectureReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _lectureReadWriteRepository = LectureReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<LectureDeleteRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _lectureReadWriteRepository.RemoveRangeLecturerAsync(data, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Lecture"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Lecture")
                    }
                };
            }
        }
    }
}