using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Friends.Classes
{
    public static class CacheExtension
    {
        public static T Get<T>(this Cache cache, string key, Func<string,T> generator)
        {
            if (cache[key] == null)
            {
                cache[key] = generator(key.Substring(4));
            }
            return (T)cache[key];
        }

        public static void Set(this Cache cache, string key, object obj)
        {
            cache[key] = obj;
        }
    }
}