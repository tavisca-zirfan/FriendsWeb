
using System;
using Infrastructure.Configuration;
using Infrastructure.Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTest
{
    [TestClass]
    public class ObjectFactoryTest
    {
        [TestMethod]
        public void ShouldResolveType()
        {
            var obj = ObjectFactory.Resolve<IMockA>("mockAv3");
            Assert.IsInstanceOfType(obj,typeof(MockAv3));
        }

    }

    public class TestContainerSetup : IContainerSetup
    {
        public void Setup(Infrastructure.Container.IDependencyContainer container)
        {
            container.Register<IMockA, MockAv3>("mockAv3");
        }
    }

    public class MockAv3 : IMockA
    {
        public string GetName()
        {
            return "MockAv3";
        }
    }


}
