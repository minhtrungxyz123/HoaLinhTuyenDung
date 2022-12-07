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
    }
}