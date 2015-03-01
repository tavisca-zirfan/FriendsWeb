using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Configuration;
using Microsoft.Practices.Unity;

namespace Infrastructure.Container
{
    public interface IDependencyContainerFactory
    {
        IDependencyContainer CreateContainer(ContainerSetting setting);
    }

    public class DependencyContainerFactory : IDependencyContainerFactory
    {
        public IDependencyContainer CreateContainer(ContainerSetting setting)
        {
            var unity = new UnityContainer();
            var dependencyContainer = new DependencyContainer(unity);
            dependencyContainer.Configure(setting);
            return new DependencyContainer(unity.CreateChildContainer());
        }

        public IDependencyContainer CreateContainer()
        {
            var setting = ContainerSetting.Load();
            return CreateContainer(setting);
        }
    }


}
