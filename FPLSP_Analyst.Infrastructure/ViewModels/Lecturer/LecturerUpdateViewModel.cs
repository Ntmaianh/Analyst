using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Lecturer
{
    public class LecturerUpdateViewModel : ViewModelBase<LectureUpdateRequest>
    {
        private readonly ILecturerReadWriteRepository _LectureReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerUpdateViewModel(ILecturerReadWriteRepository LectureReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _LectureReadWriteRepository = LectureReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(LectureUpdateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _LectureReadWriteRepository.UpdateLecturerAsync(_mapper.Map<LecturerEntity>(request), cancellationToken);

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
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Lecture")
                    }
                };
            }
        }
    }
}
