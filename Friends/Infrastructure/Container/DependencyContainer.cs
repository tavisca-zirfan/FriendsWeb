using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Infrastructure.Container
{
    public class DependencyContainer:IDependencyContainer
    {
        private IUnityContainer unity;

        public DependencyContainer()
        {
            unity = new UnityContainer();
        }

        public DependencyContainer(IUnityContainer container)
        {
            unity = container;
            unity.re
        }
    }
}
