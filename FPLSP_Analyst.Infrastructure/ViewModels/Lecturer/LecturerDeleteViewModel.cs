using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerDeleteViewModel : ViewModelBase<LectureDeleteRequest>
    {
        private readonly ILecturerReadWriteRepository _lectureReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerDeleteViewModel(ILecturerReadWriteRepository LectureReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _lectureReadWriteRepository = LectureReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(LectureDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _lectureReadWriteRepository.DeleteLecturerAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the lecture"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "lecture")
                    }
                };
            }
        }
    }
}
