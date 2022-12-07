using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Vote
    {
        public int KnowledgeBaseId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
