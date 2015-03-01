using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using Infrastructure.Container;

namespace Infrastructure.Configuration
{
    public class ContainerSetting:ConfigurationSection
    {
        [ConfigurationProperty("factory",DefaultValue = null,IsRequired=true)]
        public string Factory
        {
            get { return this["factory"] as string; }
            set { this["factory"] = value; }
        }
        [ConfigurationProperty("modules")]
        [ConfigurationCollection(typeof(ModuleCollection),AddItemName = "add")]
        public ModuleCollection ModuleCollection
        {
            get { return this["modules"] as ModuleCollection; }
            set { this["modules"] = value; }
        }

        public IDependencyContainerFactory CreateFactory()
        {
            var factory = Activator.CreateInstance(Type.GetType(this.Factory)) as IDependencyContainerFactory;
            return factory;
        }

        public static ContainerSetting Load()
        {
            return ConfigurationManager.GetSection("container.setting") as ContainerSetting;
        }
    }

    public class ModuleCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Module();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Module) element).Name;
        }
    }


    public class Module : ConfigurationElement
    {
        [ConfigurationProperty("name",DefaultValue = null,IsRequired=true)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }
        [ConfigurationProperty("type",IsRequired = true,DefaultValue = null)]
        public string Type
        {
            get { return this["type"] as string; }
            set { this["type"] = value; }
        }

        public void Configure(IDependencyContainer container)
        {
            var containerSetup = Activator.CreateInstance(System.Type.GetType(this.Type)) as IContainerSetup;
            if(containerSetup!=null)
                containerSetup.Setup(container);
        }
    }
}
