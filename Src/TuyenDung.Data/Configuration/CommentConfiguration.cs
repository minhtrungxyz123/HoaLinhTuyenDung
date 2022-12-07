using TuyenDung.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TuyenDung.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> entity)
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Content).HasMaxLength(500);

            entity.Property(e => e.OwnerUserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}