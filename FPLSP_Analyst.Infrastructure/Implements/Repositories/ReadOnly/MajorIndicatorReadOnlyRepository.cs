using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator.request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ValueObjects.Pagination;
using FPLSP_Analyst.Application.ValueObjects.Response;
using FPLSP_Analyst.Domain.Entities;
using FPLSP_Analyst.Domain.Enums;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using FPLSP_Analyst.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly
{
    public class MajorIndicatorReadOnlyRepository : IMajorIndicatorReadOnlyRepository
    {
        private readonly DbSet<MajorIndicatorEntity> _majorIndicatorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public MajorIndicatorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _majorIndicatorEntities = dbContext.Set<MajorIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<PaginationResponse<MajorIndicatorDto>>> GetMajorIndicatorWithPaginationByAdminAsync(ViewMajorIndicatorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _majorIndicatorEntities.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted && !x.Deleted).ProjectTo<MajorIndicatorDto>(_mapper.ConfigurationProvider);
                if (request.SemesterId != Guid.Empty)
                {
                    query = query.Where(x => x.SemesterId == request.SemesterId);
                }
                var result = await query.PaginateAsync(request, cancellationToken);

                return RequestResult<PaginationResponse<MajorIndicatorDto>>.Succeed(new PaginationResponse<MajorIndicatorDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorIndicatorDto>>.Fail(_localizationService["List of majorIndicator are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of majorIndicator"
                    }
                });
            }
        }

        public async Task<RequestResult<MajorIndicatorDto>> GetMajorIndicatorWithPaginationBySearchAsync(MajorIndicatorWithPaginationBySearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.SemesterId == Guid.Empty)
                {
                    return RequestResult<MajorIndicatorDto>.Fail(_localizationService["Must have semester id"]);
                }
                var major = _majorIndicatorEntities.AsNoTracking().Where(c => !c.Deleted && c.Status != EntityStatus.Deleted).ProjectTo<MajorIndicatorDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault(x => x.SemesterId == request.SemesterId);

                return RequestResult<MajorIndicatorDto>.Succeed();
            }
            catch (Exception e)
            {
                return RequestResult<MajorIndicatorDto>.Fail(_localizationService["MajorIndicator is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "MajorIndicator"
                    }
                });
            }
        }
    }
}
