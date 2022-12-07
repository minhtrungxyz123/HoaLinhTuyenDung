using TuyenDung.ModelName.XBaseModel;

namespace TuyenDung.Model
{
    public class AttachmentModel
    {
        public int Id { get; set; }

        [XBaseResourceDisplayName("Mã vạch")]
        public string FileName { get; set; } = null!;

        public string FilePath { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public long FileSize { get; set; }
        public int KnowledgeBaseId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}