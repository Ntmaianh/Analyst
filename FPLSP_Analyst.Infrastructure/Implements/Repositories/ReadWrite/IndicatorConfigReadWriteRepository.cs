using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
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
    public class IndicatorConfigReadWriteRepository : IIndicatorConfigReadWriteRepository
    {

        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public IndicatorConfigReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddIndicatorConfigAsync(IndicatorConfigEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.IndicatorConfigs.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create IndicatorConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "IndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteIndicatorConfigAsync(IndicatorConfigDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed indicatorConfig
                var indicatorConfig = await GetIndicatorConfigByIdAsync(request.Id, cancellationToken);

                // Update value to existed indicatorConfig
                indicatorConfig!.Deleted = true;
                indicatorConfig.DeletedTime = DateTimeOffset.UtcNow;
                indicatorConfig.Status = EntityStatus.Deleted;

                _dbContext.IndicatorConfigs.Update(indicatorConfig);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete IndicatorConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "IndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateIndicatorConfigAsync(IndicatorConfigEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed indicatorConfig
                var indicatorConfig = await GetIndicatorConfigByIdAsync(entity.Id, cancellationToken);

                // Update value to existed indicatorConfig
                
                indicatorConfig!.Table = entity.Table;
                indicatorConfig.Column = entity.Column;
                indicatorConfig.Predication = entity.Predication;
                indicatorConfig.ModifiedBy = entity.ModifiedBy;
                indicatorConfig.ModifiedTime = DateTimeOffset.UtcNow;
                _dbContext.IndicatorConfigs.Update(indicatorConfig);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update IndicatorConfig"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "IndicatorConfig"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> RemoveRangeIndicatorConfigAsync(List<IndicatorConfigDeleteRequest> listIndicatorConfigRequest, CancellationToken cancellationToken)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var indicatorConfig in listIndicatorConfigRequest)
                    {
                        var recordsToDelete = _dbContext.IndicatorConfigs.Where(x => x.Id == indicatorConfig.Id && !x.Deleted).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.IndicatorConfigs.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToDelete = _dbContext.IndicatorConfigs.Where(x => x.Id == indicatorConfig.Id && !x.Deleted).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete indicatorConfig"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "indicatorConfig"
                    }
                });
                }
            }
        }
        private async Task<IndicatorConfigEntity?> GetIndicatorConfigByIdAsync(Guid idIndicatorConfig, CancellationToken cancellationToken)
        {
            var indicatorConfig = await _dbContext.IndicatorConfigs.FirstOrDefaultAsync(c => c.Id == idIndicatorConfig && !c.Deleted, cancellationToken);

            return indicatorConfig;
        }

    }
}
