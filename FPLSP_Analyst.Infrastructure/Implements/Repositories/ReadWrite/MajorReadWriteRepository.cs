using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
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
    public class MajorReadWriteRepository : IMajorReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public MajorReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddMajorAsync(MajorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = new Guid();
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.Majors.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create major"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<List<Guid>>> CreateRangeMajorAsync(List<MajorEntity> ListMajorEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Batch Size for delete and insert operations
                    int batchSize = 1000;

                    // Insert new records in batches// Delete old records in batches
                    var recordsToInsert = ListMajorEntity; // Get the records from Excel ; GetRecordsFromExcel chính là cái list mình truyền vào 
                    var index = 0;

                    while (index < recordsToInsert.Count)
                    {
                        var batch = recordsToInsert.Where(x => !_dbContext.Majors.Any(c => c.Code == x.Code)).Skip(index).Take(batchSize).ToList();
                        _dbContext.Majors.AddRange(batch);
                        _dbContext.SaveChanges();

                        index += batchSize;
                    }
                    transaction.Commit();
                    return RequestResult<List<Guid>>.Succeed(ListMajorEntity.Select(x => x.Id).ToList());
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<List<Guid>>.Fail(_localizationService["Unable to create major"], new[]
                     {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "major"
                    }
                });

                }
            }
        }

        public async Task<RequestResult<int>> DeleteMajorAsync(MajorDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var major = await GetMajorByIdAsync(request.Id, cancellationToken);

                major!.Deleted = true;
                major.DeletedTime = DateTimeOffset.UtcNow;
                major.Status = EntityStatus.Deleted;

                _dbContext.Majors.Update(major);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete major"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> RemoveRangeMajorAsync(List<MajorDeleteRequest> listMajorrequest, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var major in listMajorrequest)
                    {
                        var recordsToDelete = _dbContext.Majors.Where(x => x.Id == major.Id).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.Majors.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);
                            recordsToDelete = _dbContext.Majors.Where(x => x.Id == major.Id).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete major"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "major"
                    }
                });
                }
            }
        }

        public async Task<RequestResult<int>> UpdateMajorAsync(MajorEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed major
                var major = await GetMajorByIdAsync(entity.Id, cancellationToken);
                // Update value to existed major
                major.Code = entity.Code;
                major.ModifiedBy = entity.ModifiedBy;
                major.Status = entity.Status;
                _dbContext.Majors.Update(major);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update major"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRangeMajorAsync(List<MajorEntity> ListMajorEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var major in ListMajorEntity)
                    {
                        var recordsToUpDate = _dbContext.Majors.Where(x => x.Id == major.Id).Take(batchSize).ToList();

                        while (recordsToUpDate.Any())
                        {
                            foreach (var item in recordsToUpDate)
                            {
                                item.Code = major.Code;
                                item.Status = major.Status;
                            }
                            _dbContext.Majors.UpdateRange(recordsToUpDate);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToUpDate = _dbContext.Majors.Where(x => x.Id == major.Id).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete major"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "major"
                    }
                });
                }
            }
        }

        private async Task<MajorEntity?> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            var major = await _dbContext.Majors.FirstOrDefaultAsync(c => c.Id == idMajor && !c.Deleted, cancellationToken);

            return major;
        }
    }
}
