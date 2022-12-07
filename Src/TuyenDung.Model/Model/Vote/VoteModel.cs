namespace TuyenDung.Model
{
    public class VoteModel
    {
        public int KnowledgeBaseId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}