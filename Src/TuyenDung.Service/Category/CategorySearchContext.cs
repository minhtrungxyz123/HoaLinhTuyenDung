﻿namespace TuyenDung.Service
{
    public class CategorySearchContext
    {
        public string? Keywords { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}