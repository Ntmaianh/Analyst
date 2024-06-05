using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class TrainingFacilityProfiles : Profile
    {
        public TrainingFacilityProfiles()
        {
            CreateMap<TrainingFacilityEntity, TrainingFacilityDto>();
            CreateMap<TrainingFacilityCreateRequest, TrainingFacilityEntity>();
            CreateMap<TrainingFacilityUpdateRequest, TrainingFacilityEntity>();
        }
    }
}
