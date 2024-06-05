using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class LectureProfiles : Profile
    {
        public LectureProfiles()
        {
            CreateMap<LecturerEntity, LecturerDto>()
                 .ForMember(des => des.MajorCode, opt => opt.MapFrom(src => src.Major!.Code));
            CreateMap<LecturerEntity, LecturerDetailsDto>();
            CreateMap<LectureCreateRequest, LecturerEntity>();
        }
    }
}
