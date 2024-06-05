using FPLSP_Analyst.Application.DataTransferObjects.Class;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.ValueObjects.Response;

namespace FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly
{
    public interface IClassReadOnlyRepository
    {
        Task<RequestResult<ClassDetailsDto?>> GetClassDetailsAsync(
           Guid idClass, CancellationToken cancellationToken);

        Task<RequestResult<List<ClassForSelectDto>>> GetClassForSelect(
           ClassForSelectRequest request, CancellationToken cancellationToken);
    }
}
