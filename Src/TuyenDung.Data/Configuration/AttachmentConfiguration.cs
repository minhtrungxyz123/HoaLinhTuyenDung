using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> entity)
        {
            entity.ToTable("Attachments");

            entity.Property(e => e.FileName).HasMaxLength(200);

            entity.Property(e => e.FilePath).HasMaxLength(200);

            entity.Property(e => e.FileType)
                .HasMaxLength(4)
                .IsUnicode(false);
        }
    }
}