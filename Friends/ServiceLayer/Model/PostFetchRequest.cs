using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Model
{
    public class PostFetchRequest
    {
        public string AuthorId { get; set; }
        public string RecipientId { get; set; }
        public int PageNumber { get; set; }
        public int TotalRecords { get; set; }
        public int RecordsPerPage { get; set; }
    }
}
