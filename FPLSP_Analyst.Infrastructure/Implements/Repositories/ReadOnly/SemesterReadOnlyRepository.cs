using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Semester;
using FPLSP_Analyst.Application.DataTransferObjects.Semester.Request;
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
    public class SemesterReadOnlyRepository : ISemesterReadOnlyRepository
    {

        private readonly DbSet<SemesterEntity> _semesterEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public SemesterReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _semesterEntities = dbContext.Set<SemesterEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<SemesterDto?>> GetSemesterByIdAsync(Guid idSemester, CancellationToken cancellationToken)
        {
            try
            {
                var Semester = await _semesterEntities.AsNoTracking().Where(c => c.Id == idSemester && !c.Deleted).ProjectTo<SemesterDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<SemesterDto?>.Succeed(Semester);
            }
            catch (Exception e)
            {
                return RequestResult<SemesterDto?>.Fail(_localizationService["Semester is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Semester"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<SemesterDto>>> GetSemesterWithPaginationByAdminAsync(ViewSemesterWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<SemesterEntity> queryable = _semesterEntities.AsNoTracking().Where(c => !c.Deleted);

                if (request.GroupConfigId != null && request.GroupConfigId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.GroupConfigId == request.GroupConfigId);
                }

                var result = await queryable.Where(c => c.Status != EntityStatus.Deleted)
                    .PaginateAsync<SemesterEntity, SemesterDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<SemesterDto>>.Succeed(new PaginationResponse<SemesterDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<SemesterDto>>.Fail(_localizationService["List of Semester are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of Semester"
                    }
                });
            }
        }

        public async Task<RequestResult<SemesterForSelectDto>> GetCurrentSemester(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _semesterEntities.AsNoTracking().Where(c => c.StartTime < DateTimeOffset.Now && DateTimeOffset.Now < c.EndTime && c.Status == EntityStatus.Active && !c.Deleted).ProjectTo<SemesterForSelectDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);

                return RequestResult<SemesterForSelectDto>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<SemesterForSelectDto>.Fail(_localizationService["SemesterDetail are not found"], new[]
                {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "SemesterDetail"
                }
            });
            }
        }

        public async Task<RequestResult<List<SemesterForSelectDto>>> GetSemesterForSelect(SemesterForSelectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _semesterEntities.AsNoTracking().Where(c => !c.Deleted).OrderBy(c => c.Status).ProjectTo<SemesterForSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                return RequestResult<List<SemesterForSelectDto>>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<List<SemesterForSelectDto>>.Fail(_localizationService["List of semesterDetail are not found"], new[]
                {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "list of semesterDetail"
                }
            });
            }
        }
    }
}
