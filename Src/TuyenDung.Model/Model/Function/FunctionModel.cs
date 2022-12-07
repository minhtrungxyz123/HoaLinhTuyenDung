namespace TuyenDung.Model
{
    public class FunctionModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int SortOrder { get; set; }
        public string? ParentId { get; set; }
        public string? Icon { get; set; }
    }
}