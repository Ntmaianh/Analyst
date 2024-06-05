using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator.Request;
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
    public class SubjectIndicatorReadOnlyRepository : ISubjectIndicatorReadOnlyRepository
    {
        private readonly DbSet<SubjectIndicatorEntity> _subjectIndicator;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public SubjectIndicatorReadOnlyRepository(AppReadOnlyDbContext dbContext, ILocalizationService localizationService, IMapper mapper)
        {
            _subjectIndicator = dbContext.Set<SubjectIndicatorEntity>();
            _localizationService = localizationService;
            _mapper = mapper;
        }

        public async Task<RequestResult<SubjectIndicatorDto>> GetSubjectIndicatorBySearchAsync(SubjectIndicatorRequest request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.SemesterId == Guid.Empty && request.SubjectId == Guid.Empty)
                {
                    return RequestResult<SubjectIndicatorDto>.Fail(_localizationService["Must have semester id"]);
                }
                var subject = _subjectIndicator.AsNoTracking().Where(c => !c.Deleted).ProjectTo<SubjectIndicatorDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault(x => x.SemesterId == request.SemesterId && x.SubjectId == request.SubjectId);

                return RequestResult<SubjectIndicatorDto>.Succeed(subject);
            }
            catch (Exception e)
            {
                return RequestResult<SubjectIndicatorDto>.Fail(_localizationService["Lecture is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "lecture"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<SubjectIndicatorDto>>> GetSubjectIndicatorWithPaginationByAdminAsync(ViewSubjectIndicatorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _subjectIndicator.AsNoTracking().Where(x => x.Status != EntityStatus.Deleted && !x.Deleted).ProjectTo<SubjectIndicatorDto>(_mapper.ConfigurationProvider);
                if (request.SemesterId != Guid.Empty && request.SubjectId != Guid.Empty && request.MajorId != Guid.Empty)
                {
                    query = query.Where(x => x.SemesterId == request.SemesterId && x.MajorId == request.MajorId && x.SubjectId == request.SubjectId);
                }
                var result = await query.PaginateAsync(request, cancellationToken);

                return RequestResult<PaginationResponse<SubjectIndicatorDto>>.Succeed(new PaginationResponse<SubjectIndicatorDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<SubjectIndicatorDto>>.Fail(_localizationService["List of subjectIndicator are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of subjectIndicator"
                    }
                });
            }
        }
    }
}
