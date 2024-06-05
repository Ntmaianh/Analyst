using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility;
using FPLSP_Analyst.Application.DataTransferObjects.TrainingFacility.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using FPLSP_Analyst.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class TraningFacilityReadOnlyRepository : ITraningFacilityReadOnlyRepository
    {
        private readonly DbSet<TrainingFacilityEntity> _trainingFacilityEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public TraningFacilityReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _trainingFacilityEntities = dbContext.Set<TrainingFacilityEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<TrainingFacilityDto?>> GetTraningFacilityByIdAsync(Guid idTraingFactility, CancellationToken cancellationToken)
        {
            try
            {
                var traingFacility = await _trainingFacilityEntities.AsNoTracking().Where(c => c.Id == idTraingFactility && !c.Deleted).ProjectTo<TrainingFacilityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<TrainingFacilityDto?>.Succeed(traingFacility);
            }
            catch (Exception e)
            {
                return RequestResult<TrainingFacilityDto?>.Fail(_localizationService["trainingFacility is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "trainingFacility"
                    }
                });
            }
        }

        public async Task<RequestResult<TrainingFacilityDto?>> GetTraningFacilityByNameAsync(string traingFactilityName, CancellationToken cancellationToken)
        {
            try
            {
                var traingFacility = await _trainingFacilityEntities.AsNoTracking().Where(c => c.Name == traingFactilityName && !c.Deleted).ProjectTo<TrainingFacilityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<TrainingFacilityDto?>.Succeed(traingFacility);
            }
            catch (Exception e)
            {
                return RequestResult<TrainingFacilityDto?>.Fail(_localizationService["trainingFacility is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "trainingFacility"
                    }
                });
            }
        }
        public async Task<RequestResult<PaginationResponse<TrainingFacilityDto>>> GetTrainingFacilityWithPaginationByAdminAsync(ViewTrainingFacilityWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _trainingFacilityEntities.AsNoTracking().Where(c => !c.Deleted)
                    .PaginateAsync<TrainingFacilityEntity, TrainingFacilityDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<TrainingFacilityDto>>.Succeed(new PaginationResponse<TrainingFacilityDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<TrainingFacilityDto>>.Fail(_localizationService["List of trainingFacility are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of trainingFacilityDto"
                    }
                });
            }
        }

    }
}
