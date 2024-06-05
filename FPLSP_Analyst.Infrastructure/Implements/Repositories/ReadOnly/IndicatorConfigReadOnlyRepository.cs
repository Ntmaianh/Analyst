using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using FPLSP_Analyst.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class IndicatorConfigReadOnlyRepository : IIndicatorConfigReadOnlyRepository
    {
        private readonly DbSet<IndicatorConfigEntity> _IndicatorConfigEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public IndicatorConfigReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _IndicatorConfigEntities = dbContext.Set<IndicatorConfigEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<IndicatorConfigDto?>> GetIndicatorConfigByIdAsync(Guid idIndicatorConfig, CancellationToken cancellationToken)
        {
            try
            {
                var IndicatorConfig = await _IndicatorConfigEntities.AsNoTracking().Where(c => c.Id == idIndicatorConfig && !c.Deleted).ProjectTo<IndicatorConfigDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<IndicatorConfigDto?>.Succeed(IndicatorConfig);
            }
            catch (Exception e)
            {
                return RequestResult<IndicatorConfigDto?>.Fail(_localizationService["IndicatorConfig is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "IndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<IndicatorConfigDto>>> GetIndicatorConfigWithPaginationByAdminAsync(ViewIndicatorConfigWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<IndicatorConfigEntity> queryable = _IndicatorConfigEntities.AsNoTracking().AsQueryable();
                var result = await _IndicatorConfigEntities.AsNoTracking().Where(c => !c.Deleted)
                    .PaginateAsync<IndicatorConfigEntity, IndicatorConfigDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<IndicatorConfigDto>>.Succeed(new PaginationResponse<IndicatorConfigDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<IndicatorConfigDto>>.Fail(_localizationService["List of IndicatorConfig are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of IndicatorConfig"
                    }
                });
            }
        }
    }
}
