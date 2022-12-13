namespace TuyenDung.Model.Model.Report
{
    public class ReportModel
    {
        public int Id { get; set; }
        public int KnowledgeBaseId { get; set; }
        public string? Content { get; set; }
        public string? ReportUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsProcessed { get; set; }
        public string? ReportUserName { get; set; }
    }
}