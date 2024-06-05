using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Domain.Entities;

namespace FPLSP_Analyst.Infrastructure.Extensions.AutoMapperProfiles
{
    public class SubjectForSelectProfile : Profile
    {
        public SubjectForSelectProfile()
        {
            CreateMap<SubjectEntity, SubjectForSelectDto>();
        }
    }
}
