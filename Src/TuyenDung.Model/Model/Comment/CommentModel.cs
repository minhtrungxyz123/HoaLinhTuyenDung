using TuyenDung.Common.Extensions;

namespace TuyenDung.Model
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int KnowledgeBaseId { get; set; }
        public string? OwnerUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? ReplyId { get; set; }
        public string? OwnerName { get; set; }
        public string? KnowledgeBaseTitle { get; set; }
        public string? KnowledgeBaseSeoAlias { get; set; }
        public Pagination<CommentModel>? Children { get; set; } = new Pagination<CommentModel>();
    }
}