using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
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
    public class TraningFacilityReadWriteRepository : ITraningFacilityReadWriteRepository
    {
        private readonly AppReadWriteDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        public TraningFacilityReadWriteRepository(ILocalizationService localizationService, AppReadWriteDbContext dbContext)
        {
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        public async Task<RequestResult<Guid>> AddTrainingFacilityAsync(TrainingFacilityEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                entity.CreatedTime = DateTimeOffset.UtcNow;
                await _dbContext.TrainingFacilities.AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<Guid>.Succeed(entity.Id);
            }
            catch (Exception e)
            {
                return RequestResult<Guid>.Fail(_localizationService["Unable to create trainingFacility"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToCreate + "trainingFacility"
                    }
                });
            }
        }
        public async Task<RequestResult<int>> DeleteTrainingFacilityAsync(TrainingFacilityDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var trainingFacility = await GetTraingFacilityByIdAsync(request.Id, cancellationToken);
                trainingFacility!.Deleted = true;
                trainingFacility.DeletedTime = DateTimeOffset.UtcNow;
                trainingFacility.Status = EntityStatus.Deleted;
                _dbContext.TrainingFacilities.Update(trainingFacility);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to delete TrainingFacility"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "TrainingFacility"
                    }
                });
            }
        }
        public async Task<RequestResult<int>> RemoveRangeTraningFacilityAsync(List<TrainingFacilityDeleteRequest> listTrainingFactityrequest, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    int batchSize = 1000;
                    foreach (var trainingFacility in listTrainingFactityrequest)
                    {
                        var recordsToDelete = _dbContext.TrainingFacilities.Where(x => x.Id == trainingFacility.Id).Take(batchSize).ToList();

                        while (recordsToDelete.Any())
                        {
                            foreach (var item in recordsToDelete)
                            {
                                item.Status = EntityStatus.Deleted;
                                item.Deleted = true;
                            }
                            _dbContext.TrainingFacilities.UpdateRange(recordsToDelete);
                            await _dbContext.SaveChangesAsync(cancellationToken);

                            recordsToDelete = _dbContext.TrainingFacilities.Where(x => x.Id == trainingFacility.Id).Take(batchSize).ToList();
                        }
                    }
                    transaction.Commit();
                    return RequestResult<int>.Succeed(1);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return RequestResult<int>.Fail(_localizationService["Unable to delete trainingFacility"], new[]
                    {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToDelete + "trainingFacility"
                    }
                });
                }
            }
        }
        public async Task<RequestResult<int>> UpdateTrainingFacilityAsync(TrainingFacilityEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var trainingFacility = await GetTraingFacilityByIdAsync(entity.Id, cancellationToken);

                trainingFacility.ModifiedBy = entity.ModifiedBy;
                trainingFacility.ModifiedTime = DateTimeOffset.UtcNow;

                _dbContext.TrainingFacilities.Update(trainingFacility);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return RequestResult<int>.Succeed(1);
            }
            catch (Exception e)
            {
                return RequestResult<int>.Fail(_localizationService["Unable to update trainingFacility"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToUpdate + "trainingFacility"
                    }
                });
            }
        }
        private async Task<TrainingFacilityEntity?> GetTraingFacilityByIdAsync(Guid idTraingFacility, CancellationToken cancellationToken)
        {
            var TrainingFacility = await _dbContext.TrainingFacilities.FirstOrDefaultAsync(c => c.Id == idTraingFacility && !c.Deleted, cancellationToken);

            return TrainingFacility;
        }
        //private List<Guid> getListIdTraningFacility(Guid id)
        //{
        //    var ListIdTrainingFacility = _dbContext.TrainingFacilities.Where(x => x.Id == id).Select(x => x.Id).ToList(); ;
        //    return ListIdTrainingFacility;
        //}

    }
}
