using FPLSP_Analyst.Domain.Entities.Base;
using FPLSP_Analyst.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Domain.Entities
{
    public class GroupIndicatorConfigEntity : EntityBase
    {
        public Guid Id { get; set; }
        public Guid GroupConfigId { get; set; }
        public Guid IndicatorConfigId { get; set; }
        public EntityPriority Priority { get; set; }
        public virtual GroupConfigEntity? GroupConfig { get; set; } 
        public virtual IndicatorConfigEntity? IndicatorConfig { get; set; } 
    }
}
