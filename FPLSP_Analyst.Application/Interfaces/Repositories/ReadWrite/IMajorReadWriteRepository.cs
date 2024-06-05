using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface IMajorReadWriteRepository
    {
        Task<RequestResult<Guid>> AddMajorAsync(MajorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateMajorAsync(MajorEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteMajorAsync(MajorDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<Guid>>> CreateRangeMajorAsync(List<MajorEntity> ListMajorEntity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRangeMajorAsync(List<MajorEntity> ListMajorEntity, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeMajorAsync(List<MajorDeleteRequest> listMajorrequest, CancellationToken cancellationToken);
    }
}
