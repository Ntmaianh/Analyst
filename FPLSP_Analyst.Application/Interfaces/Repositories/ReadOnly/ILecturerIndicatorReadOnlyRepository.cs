using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ILecturerIndicatorReadOnlyRepository
    {

        Task<RequestResult<LecturerIndicatorDto?>> GetLecturerIndicatorWithPaginationBySearchAsync(
           ViewLecturerIndicatorRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<LecturerIndicatorDto>>> GetLecturerIndicatorWithPaginationByAdminAsync(
           ViewLecturerIndicatorWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
