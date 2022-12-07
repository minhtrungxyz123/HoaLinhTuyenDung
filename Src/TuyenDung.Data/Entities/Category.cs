using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Category
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
