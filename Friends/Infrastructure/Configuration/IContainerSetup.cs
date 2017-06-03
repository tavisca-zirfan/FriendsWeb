using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Container;

namespace Infrastructure.Configuration
{
    public interface IContainerSetup
    {
        void Setup(IDependencyContainer container);
    }
}
