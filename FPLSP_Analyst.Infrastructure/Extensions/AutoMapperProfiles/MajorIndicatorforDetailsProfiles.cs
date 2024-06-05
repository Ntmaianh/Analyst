using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorIndicatorForDetailsProfiles : Profile
    {
        public MajorIndicatorForDetailsProfiles()
        {
            CreateMap<MajorIndicatorEntity, MajorIndicatorForDetails>()
                .ForMember(des => des.Semester, opt => opt.MapFrom(src => src.Semester!.Code));
        }
    }
}
