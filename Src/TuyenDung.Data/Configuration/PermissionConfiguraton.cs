using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class PermissionConfiguraton : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.ToTable("Permission");

            entity.HasKey(e => new { e.RoleId, e.FunctionId, e.CommandId });

            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FunctionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CommandId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}