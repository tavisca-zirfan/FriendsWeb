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
        public OrderType Order { get; set; }
        public string OrderProperty { get; set; }
        public Dictionary<string,object> FilterProperties { get; set; }

        public SearchFilter()
        {
            FilterProperties = new Dictionary<string, object>();
        }
    }
}
