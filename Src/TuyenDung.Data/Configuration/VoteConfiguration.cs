using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> entity)
        {
            entity.ToTable("Vote");

            entity.HasKey(e => new { e.KnowledgeBaseId, e.UserId });

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}