using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IClassIndicatorReadWriteRepository
    {
        Task<RequestResult<List<ClassIndicatorEntity>>> CreateRangeClassIndicatorAsync(List<ClassIndicatorEntity> ListClassIndicatorEntity, CancellationToken cancellationToken);
    }
}
