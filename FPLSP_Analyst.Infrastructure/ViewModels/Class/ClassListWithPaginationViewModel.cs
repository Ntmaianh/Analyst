using FPLSP_Analyst.Application.DataTransferObjects.Class.Request;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.Class
{
    public class ClassListWithPaginationViewModel : ViewModelBase<ViewClassWithPaginationRequest>
    {
        public override Task HandleAsync(ViewClassWithPaginationRequest data, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
