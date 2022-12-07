using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class LabelConfiguration : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> entity)
        {
            entity.ToTable("Label");

            entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entity.Property(e => e.Name).HasMaxLength(50);
        }
    }
}