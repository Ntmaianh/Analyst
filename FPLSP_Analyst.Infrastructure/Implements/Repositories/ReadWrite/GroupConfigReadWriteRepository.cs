using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadWrite
{
    public class GroupConfigReadWriteRepository : IGroupConfigReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public GroupConfigReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddGroupConfigAsync(GroupConfigEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.GroupConfigs.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create GroupConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "GroupConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteGroupConfigAsync(GroupConfigDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed GroupConfig
                var GroupConfig = await GetGroupConfigByIdAsync(request.Id, cancellationToken);

                // Update value to existed GroupConfig
                GroupConfig!.Deleted = true;
                GroupConfig.DeletedTime = DateTimeOffset.UtcNow;
                GroupConfig.Status = EntityStatus.Deleted;

                _dbContext.GroupConfigs.Update(GroupConfig);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete GroupConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "GroupConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateGroupConfigAsync(GroupConfigEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed GroupConfig
                var groupConfig = await GetGroupConfigByIdAsync(entity.Id, cancellationToken);

                // Update value to existed GroupConfig
                groupConfig.Code = entity.Code;
                groupConfig.ModifiedBy = entity.ModifiedBy;
                groupConfig.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.GroupConfigs.Update(groupConfig);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update GroupConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "GroupConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> RemoveRangeGroupConfigAsync(List<GroupConfigDeleteRequest> listGroupConfigRequest, CancellationToken cancellationToken)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var groupConfig in listGroupConfigRequest)
                    {
                        var recordsToDelete = _dbContext.GroupConfigs.Where(x => x.Id == groupConfig.Id && !x.Deleted).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.GroupConfigs.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToDelete = _dbContext.GroupConfigs.Where(x => x.Id == groupConfig.Id && !x.Deleted).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete groupConfig"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "groupConfig"
                    }
                });
                }
            }
        }

        private async Task<GroupConfigEntity?> GetGroupConfigByIdAsync(Guid idGroupConfig, CancellationToken cancellationToken)
        {
            var GroupConfig = await _dbContext.GroupConfigs.FirstOrDefaultAsync(c => c.Id == idGroupConfig && !c.Deleted, cancellationToken);

            return GroupConfig;
        }
        private List<Guid> getListIdGroupConfigId(Guid id)
        {
            var ListIdGroupConfig = _dbContext.GroupConfigs.Where(x => x.Id == id).Select(x => x.Id).ToList(); ;
            return ListIdGroupConfig;
        }
    }
}
