
using Infrastructure.Model;

namespace ServiceLayer.Model
{
    public class ListRequest
    {
        public int PageNumber { get; set; }
        public int RecordsPerPage { get; set; }
        public OrderType Order { get; set; }
        public string OrderProperty { get; set; }
    }
}
