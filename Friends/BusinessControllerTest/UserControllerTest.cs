using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using DomainService;
using DAL;
using Infrastructure.Data;
using BusinessDomain.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessControllerTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void GetUserByCredentials()
        {
            var controller = new UserController();
            var ur = new Mock<IUserRepository>();
            ur.Setup(m => m.GetUser(It.IsAny<string>())).Returns(new User {Email = "zaid.haq@gmail.com", Password = "abcd"});
            controller.UserRepository = ur.Object;
            var user = controller.GetUser("Zaid", "abcd");
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void GetNullUserByIncorrectCredentials()
        {
            var controller = new UserController();
            var ur = new Mock<IUserRepository>();
            ur.Setup(m => m.GetUser(It.IsAny<string>())).Returns(new User { Email = "zaid.haq@gmail.com", Password = "abcd" });
            controller.UserRepository = ur.Object;
            var user = controller.GetUser("Zaid", "abcd123");
            Assert.IsNull(user);
        }
        [TestMethod]
        public void GetNullUserIfUserNotExist()
        {
            var controller = new UserController();
            var ur = new Mock<IUserRepository>();
            ur.Setup(m => m.GetUser(It.IsAny<string>())).Returns(()=> null);
            controller.UserRepository = ur.Object;
            var user = controller.GetUser("Zaid", "abcd123");
            Assert.IsNull(user);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ThrowExceptionIfRepositoryFails()
        {
            var controller = new UserController();
            var ur = new Mock<IUserRepository>();
            ur.Setup(m => m.GetUser(It.IsAny<string>())).Throws(new Exception());
            controller.UserRepository = ur.Object;
            var user = controller.GetUser("Zaid", "abcd123");
            //Assert.IsNull(user);
        }

        [TestMethod]
        public void RegisterUserSuccessfully()
        {
            var controller = new UserController();
            var ur = new Mock<IUserRepository>();
            ur.Setup(m => m.AddUser(It.IsAny<User>())).Returns(new User{Id = "abc"});
            //ur.Setup(m => m.AddProfile(It.IsAny<string>(), It.IsAny<Profile>())).Returns(new Profile());
            ur.Setup(m => m.AddRoles(It.IsAny<string>(), It.IsAny<List<int>>()));
            var uow = new Mock<IUnitOfWork>();
            uow.Setup(m => m.Commit());
            controller.UserRepository = ur.Object;
            controller.UnitOfWork = uow.Object;
            var user = controller.RegisterUser(new User());
            Assert.IsNotNull(user);
        }
        
    }
}
