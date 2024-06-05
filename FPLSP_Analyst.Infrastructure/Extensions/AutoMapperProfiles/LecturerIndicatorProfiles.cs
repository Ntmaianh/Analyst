using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class LecturerIndicatorProfiles : Profile
    {
        public LecturerIndicatorProfiles()
        {
            CreateMap<LecturerIndicatorEntity, LecturerIndicatorDto>()
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Lecturer.Username))
                .ForMember(des => des.MajorName, opt => opt.MapFrom(src => src.Lecturer.Major.Code))
                .ForMember(des => des.SemesterCode, opt => opt.MapFrom(src => src.Semester.Code))
                .ForMember(des => des.SemesterId, opt => opt.MapFrom(src => src.Semester.Id))
                .ForMember(des => des.MajorId, opt => opt.MapFrom(src => src.Lecturer.Major.Id));
            CreateMap<LecturerIndicatorCreateRequest, LecturerIndicatorEntity>();
        }
    }
}
