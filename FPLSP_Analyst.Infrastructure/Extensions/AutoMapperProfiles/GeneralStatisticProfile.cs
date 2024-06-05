using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.GeneralStatistic;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class GeneralStatisticProfile : Profile
    {
        public GeneralStatisticProfile()
        {
            CreateMap<SemesterEntity, GeneralStatisticDto>()
                .ForMember(des => des.SemesterId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.SemesterCode, opt => opt.MapFrom(src => src.Code))
                .ForMember(des => des.MajorTotal, opt => opt.MapFrom(src => (src.MajorIndicators == null) ? 0 : src.MajorIndicators!.Count()))
                .ForMember(des => des.LecturerTotal, opt => opt.MapFrom(src => (src.LecturerIndicators == null) ? 0 : src.LecturerIndicators!.Count()))
                .ForMember(des => des.SubjectTotal, opt => opt.MapFrom(src => (src.SubjectIndicators == null) ? 0 : src.SubjectIndicators!.Count()))
                .ForMember(des => des.ClassTotal, opt => opt.MapFrom(src => (src.ClassIndicators == null) ? 0 : src.ClassIndicators!.Count()))
                ;
        }
    }
}
