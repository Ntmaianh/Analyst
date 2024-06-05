using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IMajorIndicatorReadWriteRepository
    {
        Task<RequestResult<List<MajorIndicatorEntity>>> CreateRangeMajorIndicatorAsync(List<MajorIndicatorEntity> ListMajorindicator, CancellationToken cancellationToken);
    }
}
