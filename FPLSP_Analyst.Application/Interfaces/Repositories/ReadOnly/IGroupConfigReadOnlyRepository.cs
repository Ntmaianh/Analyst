using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IGroupConfigReadOnlyRepository
    {
        Task<RequestResult<GroupConfigDto?>> GetGroupConfigByIdAsync(Guid idGroupConfig, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<GroupConfigDto>>> GetGroupConfigWithPaginationByAdminAsync(
            ViewGroupConfigWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
