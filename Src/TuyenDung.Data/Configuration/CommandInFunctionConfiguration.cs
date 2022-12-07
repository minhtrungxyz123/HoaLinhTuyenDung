using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class CommandInFunctionConfiguration : IEntityTypeConfiguration<CommandInFunction>
    {
        public void Configure(EntityTypeBuilder<CommandInFunction> entity)
        {
            entity.ToTable("CommandInFunction");

            entity.HasKey(e => new { e.CommandId, e.FunctionId });

            entity.Property(e => e.CommandId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.FunctionId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}