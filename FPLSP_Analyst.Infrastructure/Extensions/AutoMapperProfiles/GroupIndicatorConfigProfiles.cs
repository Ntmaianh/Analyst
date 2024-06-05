using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig;
using FPLSP_Analyst.Application.DataTransferObjects.GroupIndicatorConfig.Request;
using FPLSP_Analyst.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class GroupIndicatorConfigProfiles : Profile
    {
        public GroupIndicatorConfigProfiles()
        {
            CreateMap<GroupIndicatorConfigEntity, GroupIndicatorConfigDTO>();
            CreateMap<GroupIndicatorConfigCreateRequest, GroupIndicatorConfigEntity>();
            CreateMap<GroupIndicatorConfigUpdateRequest, GroupIndicatorConfigEntity>();
        }
    }
}
