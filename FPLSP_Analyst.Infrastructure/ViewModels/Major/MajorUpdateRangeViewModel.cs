using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorUpdateRangeViewModel : ViewModelBase<List<MajorUpdateRequest>>
    {
        private readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorUpdateRangeViewModel(IMajorReadWriteRepository majorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorReadWriteRepository = majorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<MajorUpdateRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var upDateResult = await _majorReadWriteRepository.UpdateRangeMajorAsync(_mapper.Map<List<MajorEntity>>(data), cancellationToken);
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
                        Error = _localizationService["Error occurred while updating the Major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToUpdate, "Major")
                    }
                };
            }
        }
    }
}