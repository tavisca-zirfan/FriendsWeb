using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace DAL
{
    public static class FilterExtension
    {
        public static object Get(this SearchFilter filter, string key)
        {
            var value = filter.FilterProperties.FirstOrDefault(f=>f.Name==key);
            return value != null ? value.Value : null;
        }
    }
}
