using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class GroupConfigProfiles : Profile
    {
        public GroupConfigProfiles()
        {
            CreateMap<GroupConfigEntity, GroupConfigDto>()
                               .ForMember(des => des.NumberOfSemester,
                                  opt => opt.MapFrom(src => src.Semesters == null ? 0 : src.Semesters!.Where(c => c.Status != EntityStatus.Deleted).Count()));
            CreateMap<GroupConfigCreateRequest, GroupConfigEntity>();
            CreateMap<GroupConfigUpdateRequest, GroupConfigEntity>();
        }
    }
}
