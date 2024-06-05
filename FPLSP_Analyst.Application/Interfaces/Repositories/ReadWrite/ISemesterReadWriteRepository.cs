using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface ISemesterReadWriteRepository
    {
        Task<RequestResult<Guid>> AddSemesterAsync(SemesterEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRangeStatusOfSemesterAsync(Guid idActiveSemester, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateSemesterAsync(SemesterEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteSemesterAsync(SemesterDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeSemesterAsync(List<SemesterDeleteRequest> listSemesterrequestt, CancellationToken cancellationToken);
    }
}
