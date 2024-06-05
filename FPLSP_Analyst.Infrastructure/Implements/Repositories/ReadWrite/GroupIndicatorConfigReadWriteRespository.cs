using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadWrite
{
    public class GroupIndicatorConfigReadWriteRespository : IGroupIndicatorConfigReadWriteRespository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;
        public GroupIndicatorConfigReadWriteRespository(AppReadWriteDbContext dbContext, ILocalizationService localizationService)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<Guid>> AddGroupIndicatorConfigAsync(GroupIndicatorConfigEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.GroupIndicatorConfigs.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {

                return RequestResult<Guid>.Fail(_localizationService["Unable to create GroupIndicatorConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "GroupIndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteGroupIndicatorConfigByIdAsync(GroupIndicatorConfigDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Get GroupIndicatorConfig by Id
                var groupIndicatorConfig = await GetGroupIndicatorConfigByIdAsync(request.Id, cancellationToken);
                //Change status to deleted
                groupIndicatorConfig!.Deleted = true;
                groupIndicatorConfig.Status = EntityStatus.Deleted;
                groupIndicatorConfig.DeletedTime = DateTimeOffset.UtcNow;
                //Update GroupIndicatorConfig
                _dbContext.GroupIndicatorConfigs.Update(groupIndicatorConfig);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to delete GroupIndicatorConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "GroupIndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateGroupIndicatorConfigAsync(GroupIndicatorConfigEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                //Get GroupIndicatorConfig by Id
                var groupIndicatorConfig = await GetGroupIndicatorConfigByIdAsync(entity.Id, cancellationToken);
                //Update GroupIndicatorConfig
                groupIndicatorConfig!.GroupConfigId = entity.GroupConfigId;
                groupIndicatorConfig.IndicatorConfigId = entity.IndicatorConfigId;
                groupIndicatorConfig.Priority = entity.Priority;
                groupIndicatorConfig.ModifiedTime = DateTimeOffset.UtcNow;
                //Update GroupIndicatorConfig in database
                _dbContext.GroupIndicatorConfigs.Update(groupIndicatorConfig);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);

            }
            catch (Exception e)
            {

                return RequestResult<int>.Fail(_localizationService["Unable to update GroupIndicatorConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "GroupIndicatorConfig"
                    }
                });
            }
        }
        private async Task<GroupIndicatorConfigEntity?> GetGroupIndicatorConfigByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.GroupIndicatorConfigs.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted, cancellationToken);
        }
    }
}
