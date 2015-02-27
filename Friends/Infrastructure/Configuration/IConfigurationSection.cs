using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Infrastructure.Configuration
{
    public interface IConfigurationSection
    {
        bool TryGet(string key, out string value);
    }

    public class NameValueConfigurationSection : IConfigurationSection
    {
        public NameValueCollection NameValue { get; set; }
        public string Name { get; set; }

        public NameValueConfigurationSection(string name,NameValueCollection nameValueCollection)
        {
            NameValue = nameValueCollection;
            Name = name;
        }
        public bool TryGet(string key, out string value)
        {
            value = null;
            if (NameValue[key] == null)
                return false;
            value = NameValue[key];
            return true;
        }
    }

    public class EmptyConfigurationSection : IConfigurationSection
    {
        public bool TryGet(string key, out string value)
        {
            value = "";
            return false;
        }
    }


}
