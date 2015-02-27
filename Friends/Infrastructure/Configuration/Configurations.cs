using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Configuration
{
    public class Configurations
    {
        private static IConfigurationFile _source = new ConfigurableFile();

        public static IConfigurationSection Section(string sectionName)
        {
            IConfigurationSection section;
            if (_source.TryGet(sectionName,out section))
            {
                return section;
            }
            return new EmptyConfigurationSection();
        }

        public static void SetSource(IConfigurationFile source)
        {
            if(source==null)
                throw new ArgumentNullException("source");
            _source = source;
        }
    }
}
