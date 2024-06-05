using FPLSP_Analyst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLSP_Analyst.Infrastructure.Database.Configurations
{
    public class GroupIndicatorConfigConfigurations : IEntityTypeConfiguration<GroupIndicatorConfigEntity>
    {
        public void Configure(EntityTypeBuilder<GroupIndicatorConfigEntity> builder)
        {
            builder.ToTable("GroupIndicatorConfig");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.CreatedTime).HasDefaultValue(DateTimeOffset.UtcNow);
            builder.HasOne(c => c.GroupConfig)
                    .WithMany(c => c.GroupIndicatorConfigs)
                    .HasForeignKey(c => c.GroupConfigId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.IndicatorConfig).WithMany(c => c.GroupIndicatorConfigs).HasForeignKey(c => c.IndicatorConfigId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
