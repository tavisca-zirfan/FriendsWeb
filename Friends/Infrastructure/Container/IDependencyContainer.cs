using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Container
{
    public interface IDependencyContainer
    {
        bool IsRegistered(Type typeInterface,string name= null);
        bool IsRegistered<T>(string name=null);
        IDependencyContainer Register(Type typeInterface,Type typeConcrete, string name=null);
        IDependencyContainer Register<TInterface,TConcrete>(string name=null);
        IEnumerable<object> ResolveAll(Type typeInterface);
        object Resolve(Type typeInterface, string name = null);
        IEnumerable<T> ResolveAll<T>();
        T Resolve<T>(string name = null);
        IDependencyContainer RegisterInstance(Type typeInterface, object obj, string name = null);
        IDependencyContainer RegisterInstance<T>(T obj, string name = null);
        IDependencyContainer RegisterAsSingleton(Type typeInterface, object obj, string name = null);
        IDependencyContainer RegisterAsSingleton<T>(T obj, string name = null);
        IDependencyContainer RegisterAsWebSingleton<TInterface, TConcrete>(string name = null);
    }
}
