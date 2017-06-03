using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Configuration;
using Infrastructure.Events;

namespace Infrastructure
{
    public class ContainerSetup:IContainerSetup
    {
        public void Setup(Infrastructure.Container.IDependencyContainer container)
        {
            container.Register<IDispatcher, Dispatcher>()
                ;

        }
    }
}
