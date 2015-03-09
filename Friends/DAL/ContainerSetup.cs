using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using DAL.Implementation;
using DAL.Interfaces;
using Infrastructure.Configuration;

namespace DAL
{
    public class ContainerSetup:IContainerSetup
    {
        public void Setup(Infrastructure.Container.IDependencyContainer container)
        {
            container.Register<IPostTypeRepository, PostTextRepository>(PostType.PostText.ToString());
        }
    }
}
