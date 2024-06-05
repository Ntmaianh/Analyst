using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.LecturerIndicator
{
    public class LecturerIndicatorCreateRangeViewModel : ViewModelBase<List<LecturerIndicatorCreateRequest>>
    {
        private readonly ILecturerIndicatorReadWriteRepository _LectureIndicatorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public LecturerIndicatorCreateRangeViewModel(ILecturerIndicatorReadWriteRepository LectureIndicatorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _LectureIndicatorReadWriteRepository = LectureIndicatorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<LecturerIndicatorCreateRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _LectureIndicatorReadWriteRepository.CreateRangeLecturerIndicatorAsync(_mapper.Map<List<LecturerIndicatorEntity>>(data), cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the LectureIndicator"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "LectureIndicator")
                    }
                };
            }
        }
    }
}
