using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IGroupConfigReadWriteRepository
    {
        Task<RequestResult<Guid>> AddGroupConfigAsync(GroupConfigEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateGroupConfigAsync(GroupConfigEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteGroupConfigAsync(GroupConfigDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeGroupConfigAsync(List<GroupConfigDeleteRequest> listGroupConfigrequest, CancellationToken cancellationToken);

    }
}
