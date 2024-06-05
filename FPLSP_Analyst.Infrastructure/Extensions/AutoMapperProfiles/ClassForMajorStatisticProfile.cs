using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorOverview.Details;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class ClassForMajorStatisticProfile : Profile
    {
        public ClassForMajorStatisticProfile()
        {
            CreateMap<MajorEntity, ClassForMajorStatistic>()
                 .ForMember(des => des.ClassTotalNumber,
                                  opt => opt.MapFrom(src => src.Lecturers
                                 .Select(x => x.ClassIndicators!.Count()))) // tổng số lớp 

               .ForMember(des => des.ClassGreaterThan20PercentBannedNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Select(x => x.ClassIndicators!.Count(x => x.StudentBannedPercent > 20))))

                .ForMember(des => des.ClassGreaterThan20PercentBannedNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Select(x => x.ClassIndicators!.Count(x => x.StudentBannedPercent > 10))))

                .ForMember(des => des.ClassGreaterThan20PercentBannedNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Select(x => x.ClassIndicators!.Count(x => x.StudentBannedPercent < 3))));
        }
    }
}
