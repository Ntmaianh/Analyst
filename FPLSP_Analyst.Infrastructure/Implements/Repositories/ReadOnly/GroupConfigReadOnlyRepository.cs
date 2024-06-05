using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
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
    public class GroupConfigReadOnlyRepository : IGroupConfigReadOnlyRepository
    {
        private readonly DbSet<GroupConfigEntity> _GroupConfigEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public GroupConfigReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _GroupConfigEntities = dbContext.Set<GroupConfigEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<GroupConfigDto?>> GetGroupConfigByIdAsync(Guid idGroupConfig, CancellationToken cancellationToken)
        {
            try
            {
                var GroupConfig = await _GroupConfigEntities.AsNoTracking().Where(c => c.Id == idGroupConfig && !c.Deleted).ProjectTo<GroupConfigDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<GroupConfigDto?>.Succeed(GroupConfig);
            }
            catch (Exception e)
            {
                return RequestResult<GroupConfigDto?>.Fail(_localizationService["GroupConfig is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "GroupConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<GroupConfigDto>>> GetGroupConfigWithPaginationByAdminAsync(ViewGroupConfigWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<GroupConfigEntity> queryable = _GroupConfigEntities.AsNoTracking().AsQueryable();
                var result = await _GroupConfigEntities.AsNoTracking().Where(c => !c.Deleted)
                    .PaginateAsync<GroupConfigEntity, GroupConfigDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<GroupConfigDto>>.Succeed(new PaginationResponse<GroupConfigDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<GroupConfigDto>>.Fail(_localizationService["List of GroupConfig are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of GroupConfig"
                    }
                });
            }
        }
    }
}
