using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IGeneralStatisticReadOnlyResponsitory
    {
        Task<RequestResult<List<GeneralStatisticDto>>> GetGeneralStatisticWithPaginationByAdminAsync(
           ViewGeneralStatisticWithPaginationRequest request, CancellationToken cancellationToken);
    }
}
