using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig;
using FPLSP_Analyst.Application.DataTransferObjects.IndicatorConfig.Request;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class IndicatorConfigProfiles : Profile
    {
        public IndicatorConfigProfiles()
        {
            CreateMap<IndicatorConfigEntity, IndicatorConfigDto>();
            CreateMap<IndicatorConfigCreateRequest, IndicatorConfigEntity>();
            CreateMap<IndicatorConfigUpdateRequest, IndicatorConfigEntity>();
        }
    }
}
