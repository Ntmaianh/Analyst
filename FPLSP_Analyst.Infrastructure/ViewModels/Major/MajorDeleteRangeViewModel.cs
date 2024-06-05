using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorDeleteRangeViewModel : ViewModelBase<List<MajorDeleteRequest>>
    {
        private readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorDeleteRangeViewModel(IMajorReadWriteRepository majorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorReadWriteRepository = majorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<MajorDeleteRequest> data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorReadWriteRepository.RemoveRangeMajorAsync(data, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the Major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "Major")
                    }
                };
            }
        }
    }
}
