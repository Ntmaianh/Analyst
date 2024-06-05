using FPLSP_Analyst.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Analyst.Infrastructure.Database.AppDbContext
{
    public class AppReadWriteDbContext : DbContext
    {
        public AppReadWriteDbContext()
        {
        }

        public AppReadWriteDbContext(DbContextOptions<AppReadWriteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppReadWriteDbContext).Assembly);

            var Id = Guid.NewGuid();

            modelBuilder.Entity<TrainingFacilityEntity>()
                .HasData(new TrainingFacilityEntity
                {
                    Id = Id,
                    Code = "HN",
                    Name = "Hà Nội"
                });

            modelBuilder.Entity<GroupConfigEntity>()
                .HasData(new GroupConfigEntity
                {
                    Id = Id,
                    Code = "Config_For_FA23",
                });

            modelBuilder.Entity<SemesterEntity>()
                .HasData(new SemesterEntity
                {
                    Id = Id,
                    GroupConfigId = Id,
                    Code = "FA23",
                });
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
