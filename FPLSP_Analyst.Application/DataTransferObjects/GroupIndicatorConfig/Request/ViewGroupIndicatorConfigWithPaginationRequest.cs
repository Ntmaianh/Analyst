using FPLSP_Analyst.Application.ValueObjects.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request
{
    public class ViewGroupIndicatorConfigWithPaginationRequest : PaginationRequest
    {
        public string? SearchString { get; set; }
    }
}
