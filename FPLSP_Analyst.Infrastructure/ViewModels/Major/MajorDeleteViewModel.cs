using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Major
{
    public class MajorDeleteViewModel : ViewModelBase<MajorDeleteRequest>
    {

        private readonly IMajorReadWriteRepository _MajorReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public MajorDeleteViewModel(IMajorReadWriteRepository MajorReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _MajorReadWriteRepository = MajorReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(MajorDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _MajorReadWriteRepository.DeleteMajorAsync(request, cancellationToken);

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
                        Error = _localizationService["Error occurred while updating the major"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "major")
                    }
                };
            }
        }
    }
}
