using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class GeneralStatisticReadOnlyResponsitory : IGeneralStatisticReadOnlyResponsitory
    {
        private readonly DbSet<SemesterEntity> _semesterEntity;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public GeneralStatisticReadOnlyResponsitory(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _semesterEntity = dbContext.Set<SemesterEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<List<GeneralStatisticDto>>> GetGeneralStatisticWithPaginationByAdminAsync(ViewGeneralStatisticWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _semesterEntity.AsNoTracking().Where(c => !c.Deleted).ProjectTo<GeneralStatisticDto>(_mapper.ConfigurationProvider);

                return RequestResult<List<GeneralStatisticDto>>.Succeed(await result.ToListAsync());
            }
            catch (Exception e)
            {
                return RequestResult<List<GeneralStatisticDto>>.Fail(_localizationService["List of GeneralStatistic are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of GeneralStatistic"
                    }
                });
            }
        }
    }
}
