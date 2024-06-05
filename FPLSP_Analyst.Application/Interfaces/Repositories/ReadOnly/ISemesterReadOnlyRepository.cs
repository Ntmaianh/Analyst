using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Semester;
using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ISemesterReadOnlyRepository
    {
        Task<RequestResult<SemesterDto?>> GetSemesterByIdAsync(Guid idSemester, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<SemesterDto>>> GetSemesterWithPaginationByAdminAsync(
            ViewSemesterWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<SemesterForSelectDto>> GetCurrentSemester(CancellationToken cancellationToken);
        Task<RequestResult<List<SemesterForSelectDto>>> GetSemesterForSelect(SemesterForSelectRequest request, CancellationToken cancellationToken);
    }
}
