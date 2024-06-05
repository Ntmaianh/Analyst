using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.ClassIndicator.Request;
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
    public class ClassIndicatorReadOnlyRepository : IClassIndicatorReadOnlyRepository
    {
        private readonly DbSet<ClassIndicatorEntity> _classIndicatorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public ClassIndicatorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _classIndicatorEntities = dbContext.Set<ClassIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<ClassIndicatorDto>> GetClassIndicatorWithPaginationBySearchAsync(ClassIndicatorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.SemesterId == Guid.Empty && request.ClassId == Guid.Empty)
                {
                    return RequestResult<ClassIndicatorDto>.Fail(_localizationService["Must have semester id"]);
                }
                var classIndicator = _classIndicatorEntities.AsNoTracking().Where(c => !c.Deleted).ProjectTo<ClassIndicatorDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault(x => x.Id == request.ClassId && x.SemesterId == request.SemesterId);

                return RequestResult<ClassIndicatorDto>.Succeed(classIndicator);
            }
            catch (Exception e)
            {
                return RequestResult<ClassIndicatorDto>.Fail(_localizationService["class is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "class"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<ClassIndicatorDto>>> GetClassIndicatorWithPaginationByAdminAsync(ViewClassIndicatorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _classIndicatorEntities.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted && !x.Deleted).ProjectTo<ClassIndicatorDto>(_mapper.ConfigurationProvider);

                if (request.SemesterId != null && request.SemesterId != Guid.Empty)
                {
                    query = query.Where(x => x.SemesterId == request.SemesterId);
                }

                if (request.MajorId != null && request.MajorId != Guid.Empty)
                {
                    query = query.Where(x => x.MajorId == request.MajorId);
                }

                if (request.SubjectId != null && request.SubjectId != Guid.Empty)
                {
                    query = query.Where(x => x.SubjectId == request.SubjectId);
                }

                if (request.LecturerId != null && request.LecturerId != Guid.Empty)
                {
                    query = query.Where(x => x.LecturerId == request.LecturerId);
                }

                var result = await query.PaginateAsync(request, cancellationToken);

                return RequestResult<PaginationResponse<ClassIndicatorDto>>.Succeed(new PaginationResponse<ClassIndicatorDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<ClassIndicatorDto>>.Fail(_localizationService["List of classIndicator are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of classIndicator"
                    }
                });
            }
        }
    }
}
