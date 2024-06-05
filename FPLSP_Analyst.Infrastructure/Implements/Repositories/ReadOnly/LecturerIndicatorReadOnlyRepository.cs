using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator;
using FPLSP_Analyst.Application.DataTransferObjects.LecturerIndicator.Request;
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
    public class LecturerIndicatorReadOnlyRepository : ILecturerIndicatorReadOnlyRepository
    {
        private readonly DbSet<LecturerIndicatorEntity> _lecturerIndicatorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public LecturerIndicatorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _lecturerIndicatorEntities = dbContext.Set<LecturerIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }


        public async Task<RequestResult<LecturerIndicatorDto?>> GetLecturerIndicatorWithPaginationBySearchAsync(ViewLecturerIndicatorRequest request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.SemesterId == Guid.Empty && request.LecturerId == Guid.Empty)
                {
                    return RequestResult<LecturerIndicatorDto>.Fail(_localizationService["Must have semester id"]);
                }
                var lecture = _lecturerIndicatorEntities.AsNoTracking().Where(c => !c.Deleted).ProjectTo<LecturerIndicatorDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefault(x => x.SemesterId == request.SemesterId && x.LecturerId == request.LecturerId);

                return RequestResult<LecturerIndicatorDto?>.Succeed(lecture);
            }
            catch (Exception e)
            {
                return RequestResult<LecturerIndicatorDto?>.Fail(_localizationService["LectureIndicator is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "lecture"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<LecturerIndicatorDto>>> GetLecturerIndicatorWithPaginationByAdminAsync(ViewLecturerIndicatorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var lectureIndicator = _lecturerIndicatorEntities.AsNoTracking().Where(c => !c.Deleted).ProjectTo<LecturerIndicatorDto>(_mapper.ConfigurationProvider);

                if (request.SemesterId != Guid.Empty && request.SemesterId != null)
                {
                    lectureIndicator = lectureIndicator.Where(x => x.SemesterId == request.SemesterId);
                }
                if (request.MajorId != Guid.Empty && request.MajorId != null)
                {
                    lectureIndicator = lectureIndicator.Where(x => x.MajorId == request.MajorId);
                }
                if (request.LecturerId != Guid.Empty && request.LecturerId != null)
                {
                    lectureIndicator = lectureIndicator.Where(x => x.LecturerId == request.LecturerId);
                }

                var result = await lectureIndicator.PaginateAsync(request, cancellationToken);
                return RequestResult<PaginationResponse<LecturerIndicatorDto>>.Succeed(new PaginationResponse<LecturerIndicatorDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<LecturerIndicatorDto>>.Fail(_localizationService["List of lecturerIndicator are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of lecturerIndicator"
                    }
                });
            }
        }

    }
}
