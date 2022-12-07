namespace TuyenDung.Data.Entities
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? UserId { get; set; }
        public string? Content { get; set; }
    }
}
