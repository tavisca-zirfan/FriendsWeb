using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Model
{
    public class PostFetchRequest:ListRequest
    {
        public string AuthorId { get; set; }
        public string Text { get; set; }
    }
}
