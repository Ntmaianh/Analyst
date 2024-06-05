using FPLSP_Analyst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Analyst.Infrastructure.Database.Configurations
{
    public partial class TrainingFacilityEntityConfigurations : IEntityTypeConfiguration<TrainingFacilityEntity>
    {
        public void Configure(EntityTypeBuilder<TrainingFacilityEntity> builder)
        {
            builder.ToTable("TrainingFacility");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedTime).HasDefaultValue(DateTimeOffset.UtcNow);
        }
    }
}