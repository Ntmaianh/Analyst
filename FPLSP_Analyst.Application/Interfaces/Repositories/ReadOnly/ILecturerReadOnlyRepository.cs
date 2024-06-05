using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ILecturerReadOnlyRepository
    {
        Task<RequestResult<LecturerDto?>> GetLecturerByIdAsync(Guid idLecture, CancellationToken cancellationToken);
        Task<RequestResult<List<LecturerDto>>> GetListLecturerByIdAsync(List<Guid> ListIdLecture, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<LecturerDto>>> GetLecturerWithPaginationByAdminAsync(
            ViewLecturerWithPaginationRequest request, CancellationToken cancellationToken);

        Task<RequestResult<List<LecturerForSelectDto>>> GetLecturerForSelect(LecturerForSelectRequest request, CancellationToken cancellationToken);

        Task<RequestResult<LecturerDetailsDto?>> GetLecturerDetailsWithPaginationByAdminAsync(ViewLecturerDetailsWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
