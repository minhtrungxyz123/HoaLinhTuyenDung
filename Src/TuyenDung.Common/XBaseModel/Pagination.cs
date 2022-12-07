using System.Collections.Generic;
using Ticket.Common.XBaseModel;

namespace TuyenDung.Common.XBaseModel
{
    public class Pagination<T> : PaginationBase where T : class
    {
        public List<T> Items { get; set; }
    }
}