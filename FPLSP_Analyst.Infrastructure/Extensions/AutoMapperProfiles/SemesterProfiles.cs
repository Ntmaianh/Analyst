using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Semester;
using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class SemesterProfiles : Profile
    {
        public SemesterProfiles()
        {
            CreateMap<SemesterEntity, SemesterDto>()
               .ForMember(des => des.GroupConfigCode,
                                  opt => opt.MapFrom(src => src.GroupConfig!.Code));
            CreateMap<SemesterCreateRequest, SemesterEntity>();
            CreateMap<SemesterUpdateRequest, SemesterEntity>();
        }
    }
}
