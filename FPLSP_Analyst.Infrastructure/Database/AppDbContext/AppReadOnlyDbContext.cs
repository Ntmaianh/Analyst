using FPLSP_Analyst.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Database.AppDbContext
{
    public class AppReadOnlyDbContext : DbContext
    {
        public AppReadOnlyDbContext()
        {
        }

        public AppReadOnlyDbContext(DbContextOptions<AppReadOnlyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppReadOnlyDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=66.42.55.38;Initial Catalog=FPLSP_Analyst;User ID=test;Password=E=lPJeY>-g/9QxzE;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }

        #region DbSet
        public DbSet<GroupConfigEntity> GroupConfigs { get; set; }
        public DbSet<GroupIndicatorConfigEntity> GroupIndicatorConfigs { get; set; }
        public DbSet<IndicatorConfigEntity> IndicatorConfigs { get; set; }


        public DbSet<TrainingFacilityEntity> TrainingFacilities { get; set; }
        public DbSet<SemesterEntity> Semesters { get; set; }

        public DbSet<MajorEntity> Majors { get; set; }
        public DbSet<MajorIndicatorEntity> MajorIndicators { get; set; }

        public DbSet<LecturerEntity> Lecturers { get; set; }
        public DbSet<LecturerIndicatorEntity> LecturerIndicators { get; set; }

        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<SubjectIndicatorEntity> SubjectIndicators { get; set; }

        public DbSet<ClassIndicatorEntity> ClassIndicators { get; set; }
        #endregion
    }
}
