using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> entity)
        {
            entity.ToTable("Report");

            entity.Property(e => e.Content).HasMaxLength(500);

            entity.Property(e => e.ReportUserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}