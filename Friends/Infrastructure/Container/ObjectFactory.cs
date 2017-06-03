using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Configuration;

namespace Infrastructure.Container
{
    public static class ObjectFactory
    {
        private static readonly IDependencyContainer Container;
        static ObjectFactory()
        {
            var setting = ContainerSetting.Load();
            var factory = setting.CreateFactory();
            Container = factory.CreateContainer(setting);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return Container.ResolveAll<T>();
        }

        public static T Resolve<T>(string name= null)
        {
            return Container.Resolve<T>(name);
        }

        public static bool IsRegistered<T>(string name = null)
        {
            return Container.IsRegistered<T>(name);
        }
    }
}
