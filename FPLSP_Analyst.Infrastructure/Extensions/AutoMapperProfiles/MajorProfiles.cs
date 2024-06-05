using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Major;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class MajorProfiles : Profile
    {
        public MajorProfiles()
        {
            CreateMap<MajorEntity, MajorDto>()
                               .ForMember(des => des.NumberOfSubject,
                                  opt => opt.MapFrom(src => src.Subjects == null ? 0 : src.Subjects!.Where(c => c.Status != EntityStatus.Deleted).Count()))
                               .ForMember(des => des.NumberOfLecturer,
                                  opt => opt.MapFrom(src => src.Lecturers == null ? 0 : src.Lecturers!.Where(c => c.Status != EntityStatus.Deleted).Count()));
            CreateMap<MajorEntity, MajorDetailsDto>();
            CreateMap<MajorCreateRequest, MajorEntity>();
            CreateMap<MajorUpdateRequest, MajorEntity>();
        }

    }
}
