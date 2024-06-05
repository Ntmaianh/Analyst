using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator.request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorIndicatorReadOnlyRepository
    {
        Task<RequestResult<PaginationResponse<MajorIndicatorDto>>> GetMajorIndicatorWithPaginationByAdminAsync(
          ViewMajorIndicatorWithPaginationRequest request, CancellationToken cancellationToken);

        Task<RequestResult<MajorIndicatorDto>> GetMajorIndicatorWithPaginationBySearchAsync(
        MajorIndicatorWithPaginationBySearchRequest request, CancellationToken cancellationToken);


    }
}
