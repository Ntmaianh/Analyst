using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer;
using FPLSP_Analyst.Application.DataTransferObjects.Lecturer.Request;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator;
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
    public class LecturerReadOnlyRepository : ILecturerReadOnlyRepository
    {
        private readonly DbSet<LecturerEntity> _lecturerEntities;
        private readonly DbSet<LecturerIndicatorEntity> _lecturerIndicatorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public LecturerReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _lecturerEntities = dbContext.Set<LecturerEntity>();
            _lecturerIndicatorEntities = dbContext.Set<LecturerIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }
        public async Task<RequestResult<LecturerDto?>> GetLecturerByIdAsync(Guid idLecture, CancellationToken cancellationToken)
        {

            try
            {
                var lecture = await _lecturerEntities.AsNoTracking().Where(c => c.Id == idLecture && !c.Deleted).ProjectTo<LecturerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<LecturerDto?>.Succeed(lecture);
            }
            catch (Exception e)
            {
                return RequestResult<LecturerDto?>.Fail(_localizationService["Lecture is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "lecture"
                    }
                });
            }
        }
        public async Task<RequestResult<PaginationResponse<LecturerDto>>> GetLecturerWithPaginationByAdminAsync(ViewLecturerWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IQueryable<LecturerEntity> queryable = _lecturerEntities.AsNoTracking().Where(c => !c.Deleted);

                if (request.MajorId != null && request.MajorId != Guid.Empty)
                {
                    queryable = queryable.Where(x => x.MajorId == request.MajorId);
                }

                var result = await queryable.PaginateAsync<LecturerEntity, LecturerDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<LecturerDto>>.Succeed(new PaginationResponse<LecturerDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<LecturerDto>>.Fail(_localizationService["List of lecture are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of lecturerDto"
                    }
                });
            }
        }
        public async Task<RequestResult<List<LecturerDto>>> GetListLecturerByIdAsync(List<Guid> ListIdLecture, CancellationToken cancellationToken)
        {
            try
            {
                List<LecturerDto> listLecture = new();
                foreach (var item in ListIdLecture)
                {
                    var lecture = await _lecturerEntities.AsNoTracking().Where(c => c.Id == item && !c.Deleted).ProjectTo<LecturerDto>(_mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(cancellationToken);
                    listLecture.Add(lecture!);
                }
                return RequestResult<List<LecturerDto>>.Succeed(listLecture);

            }
            catch (Exception e)
            {

                return RequestResult<List<LecturerDto>>.Fail(_localizationService["lecture is not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "lecture"
                    }
                });
            }
        }

        public async Task<RequestResult<List<LecturerForSelectDto>>> GetLecturerForSelect(LecturerForSelectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.MajorId == Guid.Empty)
                {
                    return RequestResult<List<LecturerForSelectDto>>.Fail(_localizationService["Must have major id"]);
                }

                var result = await _lecturerEntities.AsNoTracking()
                    .Where(c => c.Major!.Id == request.MajorId && !c.Deleted)
                    .ProjectTo<LecturerForSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                return RequestResult<List<LecturerForSelectDto>>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<List<LecturerForSelectDto>>.Fail(_localizationService["List of lecturerDetail are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of lecturerDetail"
                    }
                });
            }
        }

        public async Task<RequestResult<LecturerDetailsDto?>> GetLecturerDetailsWithPaginationByAdminAsync(ViewLecturerDetailsWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(request.UserEmail))
                {
                    var lecturerId = _lecturerEntities.AsNoTracking().FirstOrDefault(c => request.UserEmail.Contains(c.Username) && !c.Deleted)!.Id;

                    request.LecturerId = lecturerId;
                }

                var result = await _lecturerEntities.AsNoTracking().Where(c => c.Id == request.LecturerId && !c.Deleted).ProjectTo<LecturerDetailsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                result!.LecturerIndicatorForDetails = await _lecturerIndicatorEntities.AsNoTracking().Where(c => c.LecturerId == request.LecturerId && !c.Deleted).PaginateAsync<LecturerIndicatorEntity, LecturerIndicatorForDetails>(request, _mapper, cancellationToken);

                return RequestResult<LecturerDetailsDto?>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<LecturerDetailsDto?>.Fail(_localizationService["List of lecturer Detail are not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of lecturer Detail"
                    }
                });
            }

        }
    }
}
