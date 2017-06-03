using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Container;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTest
{
    [TestClass]
    public class DependencyContainerTest
    {
        [TestMethod]
        public void ShouldRegisterType()
        {
            var container = GetContainer().Register<IMockA, MockAv1>();
            Assert.IsTrue(container.IsRegistered<IMockA>());
        }

        [TestMethod]
        public void ShouldRegisterTypeWithName()
        {
            var container = GetContainer().Register<IMockA, MockAv1>("first");
            Assert.IsTrue(container.IsRegistered<IMockA>("first"));
            Assert.IsFalse(container.IsRegistered<IMockA>());
            Assert.IsFalse(container.IsRegistered<IMockA>("second"));
        }

        [TestMethod]
        public void ShouldReturnObject()
        {
            var container = GetContainer().Register<IMockA, MockAv1>("first");
            var obj = container.Resolve<IMockA>("first");
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj,typeof(MockAv1));
        }

        [TestMethod]
        public void ShouldReturnMultipleObject()
        {
            var container = GetContainer().Register<IMockA, MockAv1>("first").Register<IMockA,MockAv2>("second");
            IEnumerable<IMockA> obj = container.ResolveAll<IMockA>();
            Assert.AreEqual(obj.Count(),2);
            Assert.IsInstanceOfType(obj.ElementAt(0), typeof(MockAv1));
            Assert.IsInstanceOfType(obj.ElementAt(1), typeof(MockAv2));
        }

        [TestMethod]
        public void ShouldReturnSingletonObject()
        {
            var container = GetContainer().RegisterAsSingleton<IMockA>(new MockAv1());
            var obj = container.Resolve<IMockA>() as MockAv1;
            obj.Name = "singleton";
            var obj2 = container.Resolve<IMockA>();
            Assert.AreEqual(obj2.GetName(),"singleton");
        }

        private IDependencyContainer GetContainer()
        {
            return new DependencyContainerFactory().CreateContainer();
        }
    }

    public interface IMockA
    {
        string GetName();
    }

    public interface IMockB
    {
        string GetName();
    }

    public class MockAv1 : IMockA
    {
        public string Name { get; set; }

        public MockAv1()
        {
            Name = "MockAv1";
        }
        public string GetName()
        {
            return Name;
        }
    }

    public class MockAv2 : IMockA
    {
        public string GetName()
        {
            return "MockAv2";
        }
    }

    public class MockBv1 : IMockB
    {
        public string GetName()
        {
            return "MockBv1";
        }
    }

    public class MockBv2 : IMockB
    {
        public string GetName()
        {
            return "MockBv2";
        }
    }

}
