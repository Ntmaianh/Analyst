using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
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
    public class SubjectReadWriteRepository : ISubjectReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public SubjectReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddSubjectAsync(SubjectEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = new Guid();
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.Subjects.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create subject"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "subject"
                    }
                });
            }
        }

        public async Task<RequestResult<List<Guid>>> CreateRangeSubjectAsync(List<SubjectEntity> ListSubjectEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Batch Size for delete and insert operations
                    int batchSize = 1000;

                    // Insert new records in batches
                    var recordsToInsert = ListSubjectEntity; // Get the records from Excel ; GetRecordsFromExcel chính là cái list mình truyền vào 
                    var index = 0;

                    while (index < recordsToInsert.Count)
                    {
                        var batch = recordsToInsert.Where(x => !_dbContext.Subjects.Any(c => c.Code == x.Code)).Skip(index).Take(batchSize).ToList();
                        _dbContext.Subjects.AddRange(batch);
                        _dbContext.SaveChanges();

                        index += batchSize;
                    }

                    transaction.Commit();
                    return RequestResult<List<Guid>>.Succeed(ListSubjectEntity.Select(x => x.Id).ToList());
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<List<Guid>>.Fail(_localizationService["Unable to create Subject"], new[]
                     {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "Subject"
                    }
                });

                }
            }
        }
        public async Task<RequestResult<int>> DeleteSubjectAsync(SubjectDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = await GetSubjectByIdAsync(request.Id, cancellationToken);
                subject!.Deleted = true;
                subject.DeletedTime = DateTimeOffset.UtcNow;
                subject.Status = EntityStatus.Deleted;

                _dbContext.Subjects.Update(subject);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete subject"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "subject"
                    }
                });
            }
        }
        public async Task<RequestResult<int>> UpdateSubjectAsync(SubjectEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var subject = await GetSubjectByIdAsync(entity.Id, cancellationToken);
                subject.Name = entity.Name;
                subject.Code = entity.Code;
                subject.ModifiedBy = entity.ModifiedBy;
                subject.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.Subjects.Update(subject);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update subject"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "subjectb"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRangeSubjectAsync(List<SubjectEntity> ListSubjectEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var subject in ListSubjectEntity)
                    {
                        var recordsToUpDate = _dbContext.Subjects.Where(x => x.Id == subject.Id).Take(batchSize).ToList();

                        while (recordsToUpDate.Any())
                        {
                            foreach (var item in recordsToUpDate)
                            {
                                item.Status = subject.Status;
                                item.Name = subject.Name;
                                item.Code = subject.Code;

                            }
                            _dbContext.Subjects.UpdateRange(recordsToUpDate);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToUpDate = _dbContext.Subjects.Where(x => x.Id == subject.Id).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete Subjects"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "Subjects"
                    }
                });
                }
            }
        }
        public async Task<RequestResult<int>> RemoveRangeSubjectAsync(List<SubjectDeleteRequest> ListSubjectrequest, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var subject in ListSubjectrequest)
                    {
                        var recordsToDelete = _dbContext.Subjects.Where(x => x.Id == subject.Id).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.Subjects.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToDelete = _dbContext.Subjects.Where(x => x.Id == subject.Id).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete Subjects"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "Subjects"
                    }
                });
                }
            }
        }
        private async Task<SubjectEntity?> GetSubjectByIdAsync(Guid idSubject, CancellationToken cancellationToken)
        {
            var subject = await _dbContext.Subjects.FirstOrDefaultAsync(c => c.Id == idSubject && !c.Deleted, cancellationToken);

            return subject;
        }

    }
}
