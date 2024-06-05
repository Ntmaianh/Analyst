using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Infrastructure.Database.AppDbContext;
using FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadOnly;
using FPLSP_Analyst.Infrastructure.Implements.Repositories.ReadWrite;
using FPLSP_Analyst.Infrastructure.Implements.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FPLSP_Analyst.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<AppReadOnlyDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddDbContextPool<AppReadWriteDbContext>(options =>
            {
                // Configure your DbContext options here
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            #region Extensions
            services.AddTransient<ILocalizationService, LocalizationService>();
            services.AddTransient<EntityStatusExtensions>();
            #endregion

            #region ReadOnly
            services.AddTransient<ITraningFacilityReadOnlyRepository, TraningFacilityReadOnlyRepository>();

            services.AddTransient<IGroupConfigReadOnlyRepository, GroupConfigReadOnlyRepository>();
            services.AddTransient<IGroupIndicatorConfigReadOnlyRespository, GroupIndicatorConfigReadOnlyRespository>();
            services.AddTransient<ISemesterReadOnlyRepository, SemesterReadOnlyRepository>();

            services.AddTransient<IFileHandlingReadOnlyRepository, FileHandlingReadOnlyRepository>();

            services.AddTransient<IMajorReadOnlyRepository, MajorReadOnlyRepository>();
            services.AddTransient<IMajorIndicatorReadOnlyRepository, MajorIndicatorReadOnlyRepository>();

            services.AddTransient<ISubjectReadOnlyRepository, SubjectReadOnlyRepository>();
            services.AddTransient<ISubjectIndicatorReadOnlyRepository, SubjectIndicatorReadOnlyRepository>();
            services.AddTransient<ISubjectReadOnlyRepository, SubjectReadOnlyRepository>();

            services.AddTransient<ILecturerReadOnlyRepository, LecturerReadOnlyRepository>();
            services.AddTransient<ILecturerIndicatorReadOnlyRepository, LecturerIndicatorReadOnlyRepository>();
            services.AddTransient<ILecturerReadOnlyRepository, LecturerReadOnlyRepository>();

            services.AddTransient<IClassReadOnlyRepository, ClassReadOnlyRepository>();
            services.AddTransient<IClassIndicatorReadOnlyRepository, ClassIndicatorReadOnlyRepository>();
            #endregion

            #region ReadWrite
            services.AddTransient<ITraningFacilityReadWriteRepository, TraningFacilityReadWriteRepository>();

            services.AddTransient<IGroupConfigReadWriteRepository, GroupConfigReadWriteRepository>();
            services.AddTransient<IGroupIndicatorConfigReadWriteRespository, GroupIndicatorConfigReadWriteRespository>();
            services.AddTransient<ISemesterReadWriteRepository, SemesterReadWriteRepository>();

            services.AddTransient<IFileHandlingReadWriteRepository, FileHandlingReadWriteRepository>();

            services.AddTransient<IMajorReadWriteRepository, MajorReadWriteRepository>();
            services.AddTransient<IMajorIndicatorReadWriteRepository, MajorIndicatorReadWriteRepository>();

            services.AddTransient<ISubjectReadWriteRepository, SubjectReadWriteRepository>();
            services.AddTransient<ISubjectIndicatorReadWriteRepository, SubjectIndicatorReadWriteRepository>();

            services.AddTransient<ILecturerReadWriteRepository, LecturerReadWriteRepository>();
            services.AddTransient<ILecturerIndicatorReadWriteRepository, LecturerIndicatorReadWriteRepository>();

            services.AddTransient<IClassIndicatorReadWriteRepository, ClassIndicatorReadWriteRepository>();
            #endregion

            #region Statistic
            services.AddTransient<IGeneralStatisticReadOnlyResponsitory, GeneralStatisticReadOnlyResponsitory>();
            #endregion

            return services;
        }
    }
}