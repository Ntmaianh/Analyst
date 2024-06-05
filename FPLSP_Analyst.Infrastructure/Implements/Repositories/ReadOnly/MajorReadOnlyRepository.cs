using AutoMapper;
using AutoMapper.QueryableExtensions;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect;
using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.DataTransferObjects.Major;
using FPLSP_Analyst.Application.DataTransferObjects.Major.Request;
using FPLSP_Analyst.Application.DataTransferObjects.MajorIndicator;
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
    public class MajorReadOnlyRepository : IMajorReadOnlyRepository
    {

        private readonly DbSet<MajorEntity> _majorEntities;
        private readonly DbSet<MajorIndicatorEntity> _majorIndicatorEntities;
        private readonly IMapper _mapper;
        private readonly ILocalizationService _localizationService;

        public MajorReadOnlyRepository(IMapper mapper, ILocalizationService localizationService, AppReadOnlyDbContext dbContext)
        {
            _majorEntities = dbContext.Set<MajorEntity>();
            _majorIndicatorEntities = dbContext.Set<MajorIndicatorEntity>();
            _mapper = mapper;
            _localizationService = localizationService;
        }

        public async Task<RequestResult<List<MajorDto>>> GetListMajorByIdAsync(List<Guid> listIdMajor, CancellationToken cancellationToken)
        {
            try
            {
                List<MajorDto> listMajor = new();
                foreach (var item in listIdMajor)
                {
                    var major = await _majorEntities.AsNoTracking().Where(c => c.Id == item && !c.Deleted).ProjectTo<MajorDto>(_mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(cancellationToken);
                    listMajor.Add(major);
                }
                return RequestResult<List<MajorDto>>.Succeed(listMajor);
            }
            catch (Exception e)
            {
                return RequestResult<List<MajorDto>>.Fail(_localizationService["Major is not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "major"
                    }
                });
            }
        }

        public async Task<RequestResult<MajorDto?>> GetMajorByIdAsync(Guid idMajor, CancellationToken cancellationToken)
        {
            try
            {
                var major = await _majorEntities.AsNoTracking().Where(c => c.Id == idMajor && !c.Deleted).ProjectTo<MajorDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

                return RequestResult<MajorDto?>.Succeed(major);
            }
            catch (Exception e)
            {
                return RequestResult<MajorDto?>.Fail(_localizationService["Major is not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "Major"
                    }
                });
            }
        }

        public async Task<RequestResult<PaginationResponse<MajorDto>>> GetMajorWithPaginationByAdminAsync(ViewMajorWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorEntities.AsNoTracking()
                    .Where(c => !c.Deleted)
                    .PaginateAsync<MajorEntity, MajorDto>(request, _mapper, cancellationToken);

                return RequestResult<PaginationResponse<MajorDto>>.Succeed(new PaginationResponse<MajorDto>()
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    HasNext = result.HasNext,
                    Data = result.Data
                });
            }
            catch (Exception e)
            {
                return RequestResult<PaginationResponse<MajorDto>>.Fail(_localizationService["List of Major are not found"], new[]
                {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of MajorDto"
                    }
                });
            }
        }

        public async Task<RequestResult<List<MajorForSelectDto>>> GetMajorForSelect(MajorForSelectRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var queryable = _majorEntities.AsNoTracking().Where(c => !c.Deleted);
                List<MajorForSelectDto> result;

                if (request.SemesterId != Guid.Empty)
                {
                    result = await queryable.Where(c => c.MajorIndicators!.Where(x => x.Semester!.Id == request.SemesterId).Select(x => x.Major!.Id).Contains(c.Id))
                    .ProjectTo<MajorForSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                }

                result = await queryable.ProjectTo<MajorForSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                return RequestResult<List<MajorForSelectDto>>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<List<MajorForSelectDto>>.Fail(_localizationService["List of majorDetail are not found"], new[]
                {
                new ErrorItem
                {
                    Error = e.Message,
                    FieldName = LocalizationString.Common.FailedToGet + "list of majorDetail"
                }
            });
            }
        }

        public async Task<RequestResult<MajorDetailsDto?>> GetMajorDetailsWithPaginationByAdminAsync(ViewMajorDetailsWithPaginationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _majorEntities.AsNoTracking().ProjectTo<MajorDetailsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(c => c.Id == request.MajorId);

                result!.MajorIndicatorForDetails = await _majorIndicatorEntities.AsNoTracking().Where(c => c.MajorId == request.MajorId && !c.Deleted).PaginateAsync<MajorIndicatorEntity, MajorIndicatorForDetails>(request, _mapper, cancellationToken);

                return RequestResult<MajorDetailsDto?>.Succeed(result!);
            }
            catch (Exception e)
            {

                return RequestResult<MajorDetailsDto?>.Fail(_localizationService["List of major Detail are not found"], new[]
               {
                    new ErrorItem
                    {
                        Error = e.Message,
                        FieldName = LocalizationString.Common.FailedToGet + "list of major Detail"
                    }
                });
            }

        }
    }
}
