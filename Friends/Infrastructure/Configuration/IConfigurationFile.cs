using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrastructure.Configuration
{
    public interface IConfigurationFile
    {
        bool TryGet(string sectionName,out IConfigurationSection section);
    }

    public class ConfigurableFile : IConfigurationFile
    {
        public bool TryGet(string sectionName, out IConfigurationSection section)
        {
            section = null;
            var configSection = ConfigurationManager.GetSection(sectionName) as NameValueCollection;
            if (configSection == null)
                return false;
            section = new NameValueConfigurationSection(sectionName,configSection);
            return true;
        }
    }

}
