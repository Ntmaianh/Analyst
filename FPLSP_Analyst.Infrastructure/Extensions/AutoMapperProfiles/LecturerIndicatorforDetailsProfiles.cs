using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class LecturerIndicatorForDetailsProfiles : Profile
    {
        public LecturerIndicatorForDetailsProfiles()
        {
            CreateMap<LecturerIndicatorEntity, LecturerIndicatorForDetails>()
                .ForMember(des => des.Semester, opt => opt.MapFrom(x => x.Semester!.Code));
        }
    }
}
