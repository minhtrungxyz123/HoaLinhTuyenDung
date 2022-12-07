using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Comment
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
