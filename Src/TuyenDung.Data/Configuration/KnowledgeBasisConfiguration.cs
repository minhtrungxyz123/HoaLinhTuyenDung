using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class KnowledgeBasisConfiguration : IEntityTypeConfiguration<KnowledgeBasis>
    {
        public void Configure(EntityTypeBuilder<KnowledgeBasis> entity)
        {
            entity.ToTable("KnowledgeBasis");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.Environment).HasMaxLength(500);

            entity.Property(e => e.ErrorMessage).HasMaxLength(500);

            entity.Property(e => e.OwnerUserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Problem).HasMaxLength(500);

            entity.Property(e => e.SeoAlias)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Title).HasMaxLength(500);

            entity.Property(e => e.Workaround).HasMaxLength(500);
        }
    }
}