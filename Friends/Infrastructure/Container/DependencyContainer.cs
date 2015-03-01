using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Configuration;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace Infrastructure.Container
{
    public class DependencyContainer:IDependencyContainer
    {
        private IUnityContainer _container;

        public DependencyContainer()
        {
            _container = new UnityContainer();
        }

        public DependencyContainer(IUnityContainer container)
        {
            _container = container;
        }

        public bool IsRegistered(Type typeInterface, string name = null)
        {
            return _container.IsRegistered(typeInterface, name);
        }

        public bool IsRegistered<T>(string name = null)
        {
            return _container.IsRegistered<T>(name);
        }

        public IDependencyContainer Register(Type typeInterface, Type typeConcrete, string name = null)
        {
            _container.RegisterType(typeInterface, typeConcrete, name);
            return this;
        }

        public IDependencyContainer Register<TInterface, TConcrete>(string name = null)
        {
            _container.RegisterType(typeof (TInterface), typeof (TConcrete), name);
            return this;
        }

        public IEnumerable<object> ResolveAll(Type typeInterface)
        {
            return _container.ResolveAll(typeInterface);
        }

        public object Resolve(Type typeInterface, string name = null)
        {
            return _container.Resolve(typeInterface, name);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _container.ResolveAll<T>();
        }

        public T Resolve<T>(string name = null)
        {
            return _container.Resolve<T>(name);
        }

        public IDependencyContainer RegisterInstance(Type typeInterface, object obj, string name = null)
        {
            _container.RegisterInstance(typeInterface, name, obj);
            return this;
        }

        public IDependencyContainer RegisterInstance<T>(T obj, string name = null)
        {
            _container.RegisterInstance(typeof (T), obj);
            return this;
        }

        public IDependencyContainer RegisterAsSingleton(Type typeInterface, object obj, string name = null)
        {
            _container.RegisterInstance(typeInterface, name, obj, new ContainerControlledLifetimeManager());
            return this;
        }

        public IDependencyContainer RegisterAsSingleton<T>(T obj, string name = null)
        {
            _container.RegisterInstance(typeof (T), name, obj, new ContainerControlledLifetimeManager());
            return this;
        }

        public void Configure(ContainerSetting setting = null)
        {
            setting = setting ?? ContainerSetting.Load();
            if (setting == null)
                return;
            setting.ModuleCollection.Cast<Module>().ForEach(m=>m.Configure(this));
        }
    }
}
