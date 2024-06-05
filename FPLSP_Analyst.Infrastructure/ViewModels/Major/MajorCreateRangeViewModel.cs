using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorCreateRangeViewModel : ViewModelBase<List<MajorCreateRangeRequest>>
    {
        private readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        private readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorCreateRangeViewModel(IMajorReadOnlyRepository majorReadOnlyRepository, IMajorReadWriteRepository majorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorReadOnlyRepository = majorReadOnlyRepository;
            _majorReadWriteRepository = majorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(List<MajorCreateRangeRequest> listData, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _majorReadWriteRepository.CreateRangeMajorAsync(_mapper.Map<List<MajorEntity>>(listData), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _majorReadOnlyRepository.GetListMajorByIdAsync(createResult.Data, cancellationToken);
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
                        Error = _localizationService["Error occurred while getting the major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "major")
                    }
                };
            }
        }
    }
}