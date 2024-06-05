using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Class;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ClassIndicatorForDetailsProfiles : Profile
    {
        public ClassIndicatorForDetailsProfiles()
        {
            CreateMap<ClassIndicatorEntity, ClassDetailsDto>()
                    .ForMember(des => des.SubjectCode, opt => opt.MapFrom(src => src.Subject.Code))
                    .ForMember(des => des.LecturerName, opt => opt.MapFrom(src => src.Lecturer.Username))
                    .ForMember(des => des.MajorName, opt => opt.MapFrom(src => src.Lecturer.Major.Code))
                    .ForMember(des => des.SemesterCode, opt => opt.MapFrom(src => src.Semester.Code))
                    .ForMember(des => des.MajorId, opt => opt.MapFrom(src => src.Lecturer.Major.Id));
        }

    }
}
