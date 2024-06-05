using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator.Request;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface ISubjectIndicatorReadOnlyRepository
    {

        Task<RequestResult<SubjectIndicatorDto>> GetSubjectIndicatorBySearchAsync(
           SubjectIndicatorRequest request, CancellationToken cancellationToken);
        Task<RequestResult<PaginationResponse<SubjectIndicatorDto>>> GetSubjectIndicatorWithPaginationByAdminAsync(
        ViewSubjectIndicatorWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
