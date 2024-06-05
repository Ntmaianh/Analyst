using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface ILecturerReadWriteRepository
    {
        Task<RequestResult<Guid>> AddLecturerAsync(LecturerEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateLecturerAsync(LecturerEntity entity, CancellationToken cancellationToken);
        Task<RequestResult<int>> DeleteLecturerAsync(LectureDeleteRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<Guid>>> CreateRangeLecturerAsync(List<LecturerEntity> ListLectureEntity, CancellationToken cancellationToken);
        Task<RequestResult<int>> UpdateRangeLecturerAsync(List<LecturerEntity> ListLectureEntity, CancellationToken cancellationToken);
        Task<RequestResult<int>> RemoveRangeLecturerAsync(List<LectureDeleteRequest> listLecturerequest, CancellationToken cancellationToken);

    }
}
