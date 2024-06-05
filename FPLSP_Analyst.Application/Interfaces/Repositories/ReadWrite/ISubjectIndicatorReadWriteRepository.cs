using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface ISubjectIndicatorReadWriteRepository
    {
        Task<RequestResult<List<SubjectIndicatorEntity>>> CreateRangeSubjectIndicatorAsync(List<SubjectIndicatorEntity> SubjectIndicatorEntity, CancellationToken cancellationToken);

    }
}
