using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Major;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorReadOnlyRepository
    {
        Task<RequestResult<MajorDto?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken);
        Task<RequestResult<List<MajorDto>>> GetListMajorByIdAsync(List<Guid> idMajor, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<MajorDto>>> GetMajorWithPaginationByAdminAsync(
            ViewMajorWithPaginationRequest request, CancellationToken cancellationToken);
        Task<RequestResult<List<MajorForSelectDto>>> GetMajorForSelect(MajorForSelectRequest request, CancellationToken cancellationToken);

        Task<RequestResult<MajorDetailsDto?>> GetMajorDetailsWithPaginationByAdminAsync(ViewMajorDetailsWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
