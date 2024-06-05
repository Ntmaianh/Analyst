using FPLSP_Analyst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Analyst.Infrastructure.Database.Configurations
{
    public partial class SemesterEntityConfigurations : IEntityTypeConfiguration<SemesterEntity>
    {
        public void Configure(EntityTypeBuilder<SemesterEntity> builder)
        {
            builder.ToTable("Semester");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedTime).HasDefaultValue(DateTimeOffset.UtcNow);
        }
    }
}