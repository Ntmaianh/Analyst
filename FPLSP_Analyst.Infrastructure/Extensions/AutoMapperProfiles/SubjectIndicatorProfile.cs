using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class SubjectIndicatorProfiles : Profile
    {
        public SubjectIndicatorProfiles()
        {
            CreateMap<SubjectIndicatorEntity, SubjectIndicatorDto>()
                .ForMember(des => des.SubjectCode, opt => opt.MapFrom(src => src.Subject.Code))
                .ForMember(des => des.SubjectId, opt => opt.MapFrom(src => src.Subject.Id))
                .ForMember(des => des.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
               .ForMember(des => des.MajorName, opt => opt.MapFrom(src => src.Subject.Major.Code))
               .ForMember(des => des.MajorId, opt => opt.MapFrom(src => src.Subject.Major.Id))
               .ForMember(des => des.SemesterCode, opt => opt.MapFrom(src => src.Semester.Code))
               .ForMember(des => des.SemesterId, opt => opt.MapFrom(src => src.Semester.Id))
                ;
        }
    }
}
