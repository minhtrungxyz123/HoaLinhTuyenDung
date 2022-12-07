﻿namespace TuyenDung.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SeoAlias { get; set; } = null!;
        public string? SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public int? ParentId { get; set; }
        public int? NumberOfTickets { get; set; }
    }
}