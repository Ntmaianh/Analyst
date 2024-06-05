using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorStatistic;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorStatistic.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IMajorForStatisticReadOnlyRespository
    {
        Task<RequestResult<List<MajorForStatisticDto>>> GetMajorForStatisticAsync(MajorForStatisticRequest request, CancellationToken cancellationToken);

    }
}
