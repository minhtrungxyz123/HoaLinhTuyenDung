using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> entity)
        {
            entity.ToTable("ActivityLog");

            entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entity.Property(e => e.Content).HasMaxLength(500);

            entity.Property(e => e.EntityId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.EntityName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}