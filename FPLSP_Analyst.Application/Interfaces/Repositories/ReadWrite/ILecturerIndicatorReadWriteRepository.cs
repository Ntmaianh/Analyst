using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite
{
    public interface ILecturerIndicatorReadWriteRepository
    {
        Task<RequestResult<List<LecturerIndicatorEntity>>> CreateRangeLecturerIndicatorAsync(List<LecturerIndicatorEntity> LecturerIndicatorEntity, CancellationToken cancellationToken);

    }
}
