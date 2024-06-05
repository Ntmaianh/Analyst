using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class SubjectIndicatorForDetailsProfiles : Profile
    {
        public SubjectIndicatorForDetailsProfiles()
        {
            CreateMap<SubjectIndicatorEntity, SubjectIndicatorForDetails>()
                .ForMember(des => des.Semester, opt => opt.MapFrom(src => src.Semester!.Code));
        }
    }
}
