using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrastructure.Model
{
    public class SearchFilter
    {
        public int PageNumber { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalRecords { get; set; }
        public OrderType Order { get; set; }
        public string OrderProperty { get; set; }
        public List<Filter> FilterProperties { get; set; }

        public SearchFilter()
        {
            FilterProperties = new List<Filter>();
            PageNumber = 1;
            RecordsPerPage = 10;
        }
    }

    public class Filter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
