using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Subject;
using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class SubjectProfiles : Profile
    {
        public SubjectProfiles()
        {
            CreateMap<SubjectEntity, SubjectDto>()
                 .ForMember(des => des.MajorCode, opt => opt.MapFrom(src => src.Major!.Code));
            CreateMap<SubjectEntity, SubjectDetailsDto>();
            CreateMap<SubjectCreateRequest, SubjectEntity>();
            CreateMap<SubjectUpdateRequest, SubjectEntity>();
        }
    }
}
