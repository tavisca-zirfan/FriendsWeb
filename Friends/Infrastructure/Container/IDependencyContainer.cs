using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Container
{
    public interface IDependencyContainer
    {
        bool IsRegistered(Type typeInterface,string name= null);
        void Register(Type typeInterface, string name=null);
        void Register<T>(T typeInterface,string name=null);
    }
}
