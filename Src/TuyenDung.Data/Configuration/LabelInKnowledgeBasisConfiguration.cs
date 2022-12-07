using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class LabelInKnowledgeBasisConfiguration : IEntityTypeConfiguration<LabelInKnowledgeBasis>
    {
        public void Configure(EntityTypeBuilder<LabelInKnowledgeBasis> entity)
        {
            entity.ToTable("LabelInKnowledgeBasis");

            entity.HasKey(e => new { e.LabelId, e.KnowledgeBaseId });

            entity.Property(e => e.LabelId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}