using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IGroupIndicatorConfigReadWriteRespository
    {
        Task<RequestResult<Guid>> AddGroupIndicatorConfigAsync(GroupIndicatorConfigEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateGroupIndicatorConfigAsync(GroupIndicatorConfigEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteGroupIndicatorConfigByIdAsync(GroupIndicatorConfigDeleteRequest request, CancellationToken cancellationToken);
    }
}
