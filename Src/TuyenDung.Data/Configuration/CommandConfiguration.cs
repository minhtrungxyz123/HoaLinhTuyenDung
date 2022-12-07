using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class CommandConfiguration : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> entity)
        {
            entity.ToTable("Command");

            entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            entity.Property(e => e.Name).HasMaxLength(50);
        }
    }
}