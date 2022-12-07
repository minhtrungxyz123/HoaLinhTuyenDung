using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.ToTable("Categories");

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.Property(e => e.SeoAlias)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.SeoDescription).HasMaxLength(500);
        }
    }
}