using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Model
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}
