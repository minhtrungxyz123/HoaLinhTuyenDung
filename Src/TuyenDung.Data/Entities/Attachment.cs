using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public long FileSize { get; set; }
        public int KnowledgeBaseId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
