using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Report
    {
        public int Id { get; set; }
        public int KnowledgeBaseId { get; set; }
        public string? Content { get; set; }
        public string? ReportUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsProcessed { get; set; }
    }
}
