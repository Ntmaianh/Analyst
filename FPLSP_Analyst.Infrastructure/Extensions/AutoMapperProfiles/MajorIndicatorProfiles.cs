using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator;
using FPLSP_Analyst.Domain.Entities;
namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorIndicatorProfiles : Profile
    {
        public MajorIndicatorProfiles()
        {
            CreateMap<MajorIndicatorEntity, MajorIndicatorDto>()
                .ForMember(des => des.MajorCode,
                           opt => opt.MapFrom(src => src.Major.Code))
                .ForMember(des => des.SemesterCode, opt => opt.MapFrom(src => src.Semester.Code))
                .ForMember(des => des.SemesterId, opt => opt.MapFrom(src => src.Semester.Id))
                .ForMember(des => des.MajorId, opt => opt.MapFrom(src => src.Major.Id))
                .ForMember(des => des.MajorCode, opt => opt.MapFrom(src => src.Major.Code));

        }
    }

}
