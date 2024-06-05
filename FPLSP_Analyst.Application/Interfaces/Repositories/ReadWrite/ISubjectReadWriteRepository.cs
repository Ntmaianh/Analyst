using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface ISubjectReadWriteRepository
    {
        Task<RequestResult<Guid>> AddSubjectAsync(SubjectEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateSubjectAsync(SubjectEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteSubjectAsync(SubjectDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<Guid>>> CreateRangeSubjectAsync(List<SubjectEntity> ListSubjectEntity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRangeSubjectAsync(List<SubjectEntity> ListSubjectEntity, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeSubjectAsync(List<SubjectDeleteRequest> listSubjectrequestt, CancellationToken cancellationToken);

    }
}
