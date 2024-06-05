using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IIndicatorConfigReadOnlyRepository
    {
        Task<RequestResult<IndicatorConfigDto?>> GetIndicatorConfigByIdAsync(Guid idIndicatorConfig, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<IndicatorConfigDto>>> GetIndicatorConfigWithPaginationByAdminAsync(
            ViewIndicatorConfigWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
