﻿namespace TuyenDung.Common.XBaseModel
{
    public class SearchModel
    {
        public string? Keywords { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}