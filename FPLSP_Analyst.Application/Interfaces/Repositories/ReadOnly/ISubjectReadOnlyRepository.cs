using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Subject;
using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ISubjectReadOnlyRepository
    {
        Task<RequestResult<SubjectDto?>> GetSubjectByIdAsync(Guid idSubject, CancellationToken cancellationToken);
        Task<RequestResult<List<SubjectDto>>> GetListSubjectByIdAsync(List<Guid> ListidSubject, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<SubjectDto>>> GetSubjectWithPaginationByAdminAsync(
            ViewSubjectWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<SubjectForSelectDto>>> GetSubjectForSelect(SubjectForSelectRequest request, CancellationToken cancellationToken);
        Task<RequestResult<SubjectDetailsDto?>> GetSubjectDetailsWithPaginationByAdminAsync(
      ViewSubjectDetailsWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
