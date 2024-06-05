using FPLSP_Analyst.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request
{
    public class GroupIndicatorConfigCreateRequest
    {
        public Guid GroupConfigId { get; set; }
        public Guid IndicatorConfigId { get; set; }
        public EntityPriority Priority { get; set; }
    }
}
