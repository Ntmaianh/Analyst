using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig;
using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IGroupIndicatorConfigReadOnlyRespository
    {
        Task<RequestResult<GroupIndicatorConfigDTO?>> GetGroupIndicatorConfigByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<GroupIndicatorConfigDTO>>> GetGroupIndicatorConfigWithPaginationByAdminAsync(ViewGroupIndicatorConfigWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
