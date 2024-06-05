using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.Class;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class ClassReadOnlyRepository : IClassReadOnlyRepository
    {
        private readonly DbSet<ClassIndicatorEntity> _classEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public ClassReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _classEntities = dbContext.Set<ClassIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<ClassDetailsDto?>> GetClassDetailsAsync(Guid idClass, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _classEntities.AsNoTracking().Where(c => c.Id == idClass && !c.Deleted).ProjectTo<ClassDetailsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

                return RequestResult<ClassDetailsDto?>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<ClassDetailsDto?>.Fail(_localizationService["List of classDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of classDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<List<ClassForSelectDto>>> GetClassForSelect(ClassForSelectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.MajorId == Guid.Empty)
                {
                    return RequestResult<List<ClassForSelectDto>>.Fail(_localizationService["Must have major id"]);
                }

                var result = await _classEntities.AsNoTracking()
                    .Where(c => c.Subject!.Major!.Id == request.MajorId && !c.Deleted)
                    .ProjectTo<ClassForSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                return RequestResult<List<ClassForSelectDto>>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<List<ClassForSelectDto>>.Fail(_localizationService["List of classDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of classDetail"
                    }
                });
            }
        }
    }
}