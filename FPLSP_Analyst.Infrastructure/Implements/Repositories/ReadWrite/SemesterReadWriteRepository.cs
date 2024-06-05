using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
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
    public class SemesterReadWriteRepository : ISemesterReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public SemesterReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddSemesterAsync(SemesterEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.Semesters.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create Semester"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "Semester"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateRangeStatusOfSemesterAsync(Guid idActiveSemester, CancellationToken cancellationToken)
        {
            try
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Get existed Semester
                        var existedSemester = await GetSemesterByIdAsync(idActiveSemester, cancellationToken);

                        // Update value to existed Semester
                        existedSemester!.Status = EntityStatus.Active;
                        //semester.ModifiedBy = entity.ModifiedBy;
                        existedSemester.ModifiedTime = DateTimeOffset.UtcNow;

                        _dbContext.Semesters.Update(existedSemester);
                        await _dbContext.SaveChangesAsync(cancellationToken);


                        int batchSize = 1000;

                        var recordsToUpdate = _dbContext.Semesters.Where(x => x.Id != idActiveSemester && x.Status == EntityStatus.Active).Take(batchSize).ToList();

                        while (recordsToUpdate.Any())
                        {
                            foreach (var semester in recordsToUpdate)
                            {
                                semester.Status = EntityStatus.InActive;
                                //semester.ModifiedBy = entity.ModifiedBy;
                                semester.ModifiedTime = DateTimeOffset.UtcNow;
                            }
                            _dbContext.Semesters.UpdateRange(recordsToUpdate);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToUpdate = _dbContext.Semesters.Where(x => x.Id != idActiveSemester && x.Status == EntityStatus.Active).Take(batchSize).ToList();
                        }

                        transaction.Commit();
                        return RequestResult<int>.Succeed(1);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return RequestResult<int>.Fail(_localizationService["Unable to delete semester"], new[]
                        {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "semester"
                    }
                });
                    }
                }
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update Semester"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "Semester"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> DeleteSemesterAsync(SemesterDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed Semester
                var Semester = await GetSemesterByIdAsync(request.Id, cancellationToken);

                // Update value to existed Semester
                Semester!.Deleted = true;
                Semester.DeletedTime = DateTimeOffset.UtcNow;
                Semester.Status = EntityStatus.Deleted;

                _dbContext.Semesters.Update(Semester);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete Semester"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "Semester"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> RemoveRangeSemesterAsync(List<SemesterDeleteRequest> listSemesterrequest, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var semester in listSemesterrequest)
                    {
                        var recordsToDelete = _dbContext.Semesters.Where(x => x.Id == semester.Id).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.Semesters.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToDelete = _dbContext.Semesters.Where(x => x.Id == semester.Id).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete semester"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "semester"
                    }
                });
                }
            }
        }

        public async Task<RequestResult<int>> UpdateSemesterAsync(SemesterEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var Semester = await GetSemesterByIdAsync(entity.Id, cancellationToken);

                Semester.Code = entity.Code;
                Semester.GroupConfigId = entity.GroupConfigId;
                Semester.StartTime = entity.StartTime;
                Semester.EndTime = entity.EndTime;
                Semester.ModifiedBy = entity.ModifiedBy;
                Semester.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.Semesters.Update(Semester);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update Semester"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "Semester"
                    }
                });
            }
        }
        private async Task<SemesterEntity?> GetSemesterByIdAsync(Guid idSemester, CancellationToken cancellationToken)
        {
            var Semester = await _dbContext.Semesters.FirstOrDefaultAsync(c => c.Id == idSemester, cancellationToken);

            return Semester;
        }

    }
}
