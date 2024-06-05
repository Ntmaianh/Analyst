using FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IClassIndicatorReadOnlyRepository
    {
        Task<RequestResult<ClassIndicatorDto>> GetClassIndicatorWithPaginationBySearchAsync(
         ClassIndicatorRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<ClassIndicatorDto>>> GetClassIndicatorWithPaginationByAdminAsync(
          ViewClassIndicatorWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
