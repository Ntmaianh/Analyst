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
    public class MajorCreateViewModel : ViewModelBase<MajorCreateRequest>
    {
        private readonly IMajorReadOnlyRepository _majorReadOnlyRepository;
        private readonly IMajorReadWriteRepository _majorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorCreateViewModel(IMajorReadOnlyRepository MajorReadOnlyRepository, IMajorReadWriteRepository MajorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _majorReadOnlyRepository = MajorReadOnlyRepository;
            _majorReadWriteRepository = MajorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(MajorCreateRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var createResult = await _majorReadWriteRepository.AddMajorAsync(_mapper.Map<MajorEntity>(data), cancellationToken);

                if (createResult.Success)
                {
                    var result = await _majorReadOnlyRepository.GetMajorByIdAsync(createResult.Data, cancellationToken);

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
                        Error = _localizationService["Error occurred while getting the Major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToGet, "Major")
                    }
                };
            }
        }
    }
}
