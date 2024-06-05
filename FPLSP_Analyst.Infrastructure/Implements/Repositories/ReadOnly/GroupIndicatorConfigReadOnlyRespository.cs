using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig;
using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using FPLSP_Analyst.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class GroupIndicatorConfigReadOnlyRespository : IGroupIndicatorConfigReadOnlyRespository
    {
        private readonly AppReadOnlyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public GroupIndicatorConfigReadOnlyRespository(AppReadOnlyDbContext dbContext, IMapper mapper, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<GroupIndicatorConfigDTO?>> GetGroupIndicatorConfigByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                //Get existing GroupIndicatorConfig and mapping to GroupIndicatorConfigDTO
                var groupIndicatorConfig = await _dbContext.GroupIndicatorConfigs.AsNoTracking().Where(x => x.Id == id && !x.Deleted).ProjectTo<GroupIndicatorConfigDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
                return RequestResult<GroupIndicatorConfigDTO?>.Succeed(groupIndicatorConfig);
            }
            catch (Exception e)
            {

                return RequestResult<GroupIndicatorConfigDTO?>.Fail(_localizationService["GroupIndicatorConfig is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "GroupIndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<GroupIndicatorConfigDTO>>> GetGroupIndicatorConfigWithPaginationByAdminAsync(ViewGroupIndicatorConfigWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Get existing GroupIndicatorConfig and mapping to GroupIndicatorConfigDTO
                var result = await _dbContext.GroupIndicatorConfigs.AsNoTracking().Where(c => !c.Deleted)
                    .PaginateAsync<GroupIndicatorConfigEntity, GroupIndicatorConfigDTO>(request, _mapper, cancellationToken);
                return RequestResult<PaginationResponse<GroupIndicatorConfigDTO>>.Succeed(new PaginationResponse<GroupIndicatorConfigDTO>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {

                return RequestResult<PaginationResponse<GroupIndicatorConfigDTO>>.Fail(_localizationService["List of GroupIndicatorConfig are not found"], new[]
                 {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of GroupIndicatorConfig"
                    }
                });
            }
        }
    }
}
