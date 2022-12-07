namespace TuyenDung.Model
{
    public class KnowledgeBasisModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string SeoAlias { get; set; } = null!;
        public string? Description { get; set; }
        public string? Environment { get; set; }
        public string? Problem { get; set; }
        public string? StepToReproduce { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Workaround { get; set; }
        public string? Note { get; set; }
        public string OwnerUserId { get; set; } = null!;
        public string? Labels { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? NumberOfComments { get; set; }
        public int? NumberOfVotes { get; set; }
        public int? NumberOfReports { get; set; }
        public int? ViewCount { get; set; }
    }
}