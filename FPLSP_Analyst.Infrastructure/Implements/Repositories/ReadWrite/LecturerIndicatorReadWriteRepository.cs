using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadWrite
{
    public class LecturerIndicatorReadWriteRepository : ILecturerIndicatorReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public LecturerIndicatorReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _dbContext = dbContext;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<List<LecturerIndicatorEntity>>> CreateRangeLecturerIndicatorAsync(List<LecturerIndicatorEntity> ListLecturerIndicatorEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Batch Size for delete and insert operations
                    int batchSize = 1000;

                    // Delete old records in batches
                    foreach (var lectureIndicator in ListLecturerIndicatorEntity)
                    {
                        var recordsToDelete = _dbContext.LecturerIndicators.Where(x => x.SemesterId == lectureIndicator.SemesterId && !x.Deleted).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var record in recordsToDelete)
                            {
                                record.Status = EntityStatus.Deleted;
                                record.Deleted = true;
                            }

                            _dbContext.LecturerIndicators.UpdateRange(recordsToDelete);
                            _dbContext.SaveChanges();

                            recordsToDelete = _dbContext.LecturerIndicators.Where(x => x.SemesterId == lectureIndicator.SemesterId && !x.Deleted).Take(batchSize).ToList();
                        }
                    }
                    // Insert new records in batches
                    var recordsToInsert = ListLecturerIndicatorEntity; // Get the records from Excel ; GetRecordsFromExcel chính là cái list mình truyền vào 
                    var index = 0;

                    while (index < recordsToInsert.Count)
                    {
                        var batch = recordsToInsert.Skip(index).Take(batchSize).ToList();
                        _dbContext.LecturerIndicators.AddRange(batch);
                        await _dbContext.SaveChangesAsync(cancellationToken);

                        index += batchSize;
                    }
                    transaction.Commit();
                    return RequestResult<List<LecturerIndicatorEntity>>.Succeed(ListLecturerIndicatorEntity);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<List<LecturerIndicatorEntity>>.Fail(_localizationService["Unable to create lectureIndicator"], new[]
                     {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "lectureIndicator"
                    }
                });
                }
            }
        }

    }
}