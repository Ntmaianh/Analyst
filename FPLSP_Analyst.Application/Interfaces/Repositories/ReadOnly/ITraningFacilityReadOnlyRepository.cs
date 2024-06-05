using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ITraningFacilityReadOnlyRepository
    {
        Task<RequestResult<TrainingFacilityDto?>> GetTraningFacilityByIdAsync(Guid idTraingFactility, CancellationToken cancellationToken);
        Task<RequestResult<TrainingFacilityDto?>> GetTraningFacilityByNameAsync(string traingFactilityName, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<TrainingFacilityDto>>> GetTrainingFacilityWithPaginationByAdminAsync(
            ViewTrainingFacilityWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
