using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Subject;
using FPLSP_Analyst.Application.DataTransferObjects.Subject.Request;
using FPLSP_Analyst.Application.DataTransferObjects.SubjectIndicator;
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
    public class SubjectReadOnlyRepository : ISubjectReadOnlyRepository
    {
        private readonly DbSet<SubjectEntity> _subjectEntities;
        private readonly DbSet<SubjectIndicatorEntity> _subjectIndicatorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;
        public SubjectReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _subjectEntities = dbContext.Set<SubjectEntity>();
            _subjectIndicatorEntities = dbContext.Set<SubjectIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<SubjectDto?>> GetSubjectByIdAsync(Guid idSubject, CancellationToken cancellationToken)
        {
            try
            {
                var subject = await _subjectEntities.AsNoTracking().Where(c => c.Id == idSubject && !c.Deleted).ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<SubjectDto?>.Succeed(subject);
            }
            catch (Exception e)
            {
                return RequestResult<SubjectDto?>.Fail(_localizationService["subject is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "subject"
                    }
                });
            }
        }

        public async Task<RequestResult<List<SubjectDto>>> GetListSubjectByIdAsync(List<Guid> ListidSubject, CancellationToken cancellationToken)
        {
            try
            {
                List<SubjectDto> listSubject = new();
                foreach (var item in ListidSubject)
                {
                    var subject = await _subjectEntities.AsNoTracking().Where(c => c.Id == item && !c.Deleted).ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(cancellationToken);
                    listSubject.Add(subject);
                }
                return RequestResult<List<SubjectDto>>.Succeed(listSubject);

            }
            catch (Exception e)
            {

                return RequestResult<List<SubjectDto>>.Fail(_localizationService["subject is not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "subject"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<SubjectDto>>> GetSubjectWithPaginationByAdminAsync(ViewSubjectWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<SubjectEntity> queryable = _subjectEntities.AsNoTracking().Where(c => !c.Deleted);

                if (request.MajorId != null && request.MajorId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.MajorId == request.MajorId);
                }

                var result = await queryable.PaginateAsync<SubjectEntity, SubjectDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<SubjectDto>>.Succeed(new PaginationResponse<SubjectDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<SubjectDto>>.Fail(_localizationService["List of Subject are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of SubjectDto"
                    }
                });
            }
        }

        public async Task<RequestResult<List<SubjectForSelectDto>>> GetSubjectForSelect(SubjectForSelectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.MajorId == Guid.Empty)
                {
                    return RequestResult<List<SubjectForSelectDto>>.Fail(_localizationService["Must have major id"]);
                }

                if (request.LecturerId == Guid.Empty)
                {
                    return RequestResult<List<SubjectForSelectDto>>.Fail(_localizationService["Must have lecturer id"]);
                }

                var result = await _subjectEntities.AsNoTracking()
                    .Where(c => c.Major!.Id == request.MajorId && !c.Deleted)
                    .ProjectTo<SubjectForSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                return RequestResult<List<SubjectForSelectDto>>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<List<SubjectForSelectDto>>.Fail(_localizationService["List of subjectDetail are not found"], new[]
                {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "list of subjectDetail"
                }
            });
            }
        }

        public async Task<RequestResult<SubjectDetailsDto?>> GetSubjectDetailsWithPaginationByAdminAsync(ViewSubjectDetailsWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectEntities.AsNoTracking().Where(c => c.Id == request.SubjectId && !c.Deleted).ProjectTo<SubjectDetailsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                result!.SubjectIndicatorForDetails = await _subjectIndicatorEntities.AsNoTracking().Where(c => c.SubjectId == request.SubjectId && !c.Deleted).PaginateAsync<SubjectIndicatorEntity, SubjectIndicatorForDetails>(request, _mapper, cancellationToken);

                return RequestResult<SubjectDetailsDto?>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<SubjectDetailsDto?>.Fail(_localizationService["List of subjectDetail are not found"], new[]
              {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of subjectDetail"
                    }
                });
            }

        }
    }
}
