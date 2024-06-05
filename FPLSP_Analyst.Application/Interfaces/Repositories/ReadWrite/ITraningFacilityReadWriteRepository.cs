using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface ITraningFacilityReadWriteRepository
    {
        Task<RequestResult<Guid>> AddTrainingFacilityAsync(TrainingFacilityEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateTrainingFacilityAsync(TrainingFacilityEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteTrainingFacilityAsync(TrainingFacilityDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeTraningFacilityAsync(List<TrainingFacilityDeleteRequest> listTrainingFacilityrequest, CancellationToken cancellationToken);

    }
}
