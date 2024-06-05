using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.ForStatistic.MajorOverview.Details;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class SubjectForMajorStatisicProfile : Profile
    {
        public SubjectForMajorStatisicProfile()
        {
            CreateMap<MajorEntity, SubjectForMajorStatistic>()
                 .ForMember(des => des.SubjectTotalNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Count())) // tổng số môn 

                .ForMember(des => des.SubjectGreaterThan20PercentBannedNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Select(x => x.SubjectIndicators!.Count(x => x.StudentBannedPercent > 20))))

                .ForMember(des => des.SubjectGreaterThan10PercentBannedNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Select(x => x.SubjectIndicators!.Count(x => x.StudentBannedPercent > 10))))

                .ForMember(des => des.SubjectLessThan3PercentBannedNumber,
                                  opt => opt.MapFrom(src => src.Subjects!.Select(x => x.SubjectIndicators!.Count(x => x.StudentBannedPercent < 3))))
                ;
        }
    }
}
