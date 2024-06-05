using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerUpdateRangeViewModel : ViewModelBase<List<LectureUpdateRequest>>
    {
        private readonly ILecturerReadWriteRepository _lectureReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerUpdateRangeViewModel(ILecturerReadWriteRepository LectureReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _lectureReadWriteRepository = LectureReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<LectureUpdateRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var upDateResult = await _lectureReadWriteRepository.UpdateRangeLecturerAsync(_mapper.Map<List<LecturerEntity>>(data), cancellationToken);
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
                        Error = _localizationService["Error occurred while updating the Lecture"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Lecture")
                    }
                };
            }

        }
    }
}
