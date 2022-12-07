using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class FunctionConfiguration : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> entity)
        {
            entity.ToTable("Function");

            entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entity.Property(e => e.Icon)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.Property(e => e.ParentId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Url).HasMaxLength(200);
        }
    }
}