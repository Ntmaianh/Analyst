using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadWrite
{

    public class ClassIndicatorReadWriteRepository : IClassIndicatorReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public ClassIndicatorReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<List<ClassIndicatorEntity>>> CreateRangeClassIndicatorAsync(List<ClassIndicatorEntity> ListClassIndicatorEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Batch Size for delete and insert operations
                    int batchSize = 1000;

                    // Delete old records in batches
                    foreach (var classIndicator in ListClassIndicatorEntity)
                    {
                        var recordsToDelete = _dbContext.ClassIndicators.Where(x => x.SemesterId == classIndicator.SemesterId && !x.Deleted).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var record in recordsToDelete)
                            {
                                record.Status = EntityStatus.Deleted;
                                record.Deleted = true;
                            }

                            _dbContext.ClassIndicators.UpdateRange(recordsToDelete);
                            _dbContext.SaveChanges();

                            recordsToDelete = _dbContext.ClassIndicators.Where(x => x.SemesterId == classIndicator.SemesterId && !x.Deleted).Take(batchSize).ToList();
                        }
                    }

                    // Insert new records in batches
                    var recordsToInsert = ListClassIndicatorEntity; // Get the records from Excel ; GetRecordsFromExcel chính là cái list mình truyền vào 
                    var index = 0;

                    while (index < recordsToInsert.Count)
                    {
                        var batch = recordsToInsert.Skip(index).Take(batchSize).ToList();
                        _dbContext.ClassIndicators.AddRange(batch);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        index += batchSize;
                    }

                    transaction.Commit();
                    return RequestResult<List<ClassIndicatorEntity>>.Succeed(ListClassIndicatorEntity);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<List<ClassIndicatorEntity>>.Fail(_localizationService["Unable to create classIndicator"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "classIndicator"
                    }
                });

                }
            }
        }
    }
}