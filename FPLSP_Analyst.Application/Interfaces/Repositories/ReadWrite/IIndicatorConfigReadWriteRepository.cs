using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IIndicatorConfigReadWriteRepository
    {
        Task<RequestResult<Guid>> AddIndicatorConfigAsync(IndicatorConfigEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateIndicatorConfigAsync(IndicatorConfigEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteIndicatorConfigAsync(IndicatorConfigDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeIndicatorConfigAsync(List<IndicatorConfigDeleteRequest> listIndicatorConfigrequest, CancellationToken cancellationToken);

    }
}
