using FPLSP_Analyst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FPLSP_Analyst.Infrastructure.Database.Configurations
{
    public partial class ClassIndicatorEntityConfigurations : IEntityTypeConfiguration<ClassIndicatorEntity>
    {
        public void Configure(EntityTypeBuilder<ClassIndicatorEntity> builder)
        {
            builder.ToTable("ClassIndicator");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedTime).HasDefaultValue(DateTimeOffset.UtcNow);

            builder.HasOne(c => c.Subject)
                    .WithMany()
                    .HasForeignKey(c => c.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}