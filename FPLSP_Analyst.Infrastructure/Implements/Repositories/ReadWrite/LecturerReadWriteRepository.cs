using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
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
    public class LecturerReadWriteRepository : ILecturerReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public LecturerReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddLecturerAsync(LecturerEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.Id = new Guid();
                entity.CreatedTime = DateTimeOffset.UtcNow;

                await _dbContext.Lecturers.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create lecture"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "lecture"
                    }
                });
            }
        }


        public async Task<RequestResult<int>> DeleteLecturerAsync(LectureDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var lecture = await GetLecturerByIdAsync(request.Id, cancellationToken);

                lecture!.Deleted = true;
                lecture.DeletedTime = DateTimeOffset.UtcNow;
                lecture.Status = EntityStatus.Deleted;

                _dbContext.Lecturers.Update(lecture);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete lecture"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "lecture"
                    }
                });
            }
        }

        public async Task<RequestResult<int>> UpdateLecturerAsync(LecturerEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                // Get existed lecture
                var lecture = await GetLecturerByIdAsync(entity.Id, cancellationToken);
                // Update value to existed lecture
                lecture.TrainingFacilityId = entity.TrainingFacilityId;
                lecture.Username = entity.Username;
                lecture.MajorId = entity.MajorId;
                lecture.ModifiedBy = entity.ModifiedBy;
                lecture.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.Lecturers.Update(lecture);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update lecture"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "lecture"
                    }
                });
            }
        }

        public async Task<RequestResult<List<Guid>>> CreateRangeLecturerAsync(List<LecturerEntity> listLecture, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Batch Size for delete and insert operations
                    int batchSize = 1000;

                    // Insert new records in batches
                    var recordsToInsert = listLecture; // Get the records from Excel ; GetRecordsFromExcel chính là cái list mình truyền vào 
                    var index = 0;

                    while (index < recordsToInsert.Count)
                    {
                        var batch = recordsToInsert.Where(x => !_dbContext.Lecturers.Any(c => c.Username == x.Username)).Skip(index).Take(batchSize).ToList();

                        _dbContext.Lecturers.AddRange(batch);
                        _dbContext.SaveChanges();
                        index += batchSize;
                    }
                    transaction.Commit();
                    return RequestResult<List<Guid>>.Succeed(listLecture.Select(x => x.Id).ToList());
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<List<Guid>>.Fail(_localizationService["Unable to create Lecturers"], new[]
                     {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "Lecturers"
                    }
                });

                }
            }
        }
        public async Task<RequestResult<int>> UpdateRangeLecturerAsync(List<LecturerEntity> ListlectureEntity, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var lecture in ListlectureEntity)
                    {
                        var recordsToUpDate = _dbContext.Lecturers.Where(x => x.Id == lecture.Id && !x.Deleted).Take(batchSize).ToList();
                        while (recordsToUpDate.Any())
                        {
                            foreach (var item in recordsToUpDate)
                            {
                                item.Status = lecture.Status;
                                item.MajorId = lecture.MajorId;
                                item.TrainingFacilityId = lecture.TrainingFacilityId;
                                item.Username = lecture.Username;
                            }
                            _dbContext.Lecturers.UpdateRange(recordsToUpDate);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToUpDate = _dbContext.Lecturers.Where(x => x.Id == lecture.Id && !x.Deleted).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete Lecturers"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "Lecturers"
                    }
                });
                }
            }
        }
        public async Task<RequestResult<int>> RemoveRangeLecturerAsync(List<LectureDeleteRequest> listLectureRequest, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var lecture in listLectureRequest)
                    {
                        var recordsToDelete = _dbContext.Lecturers.Where(x => x.Id == lecture.Id && !x.Deleted).Take(batchSize).ToList();
                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.Lecturers.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToDelete = _dbContext.Lecturers.Where(x => x.Id == lecture.Id && !x.Deleted).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete lecture"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "lecture"
                    }
                });
                }
            }
        }

        private async Task<LecturerEntity?> GetLecturerByIdAsync(Guid idLecture, CancellationToken cancellationToken)
        {
            var lecture = await _dbContext.Lecturers.FirstOrDefaultAsync(c => c.Id == idLecture && !c.Deleted, cancellationToken);

            return lecture;
        }

    }
}
