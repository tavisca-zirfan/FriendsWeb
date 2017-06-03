using System;
using System.Collections.Generic;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainObjects;
using DAL;
using DomainService;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer;
using ServiceLayer.Model;

namespace ServiceTest
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService();
            ServiceModelMapper.CreateMap();
        }
        [TestMethod]
        public void ShouldRegisterUser()
        {
            var user = this.CreateUserForCredential("zbi@g.com", "123456");
            var mockController = new Mock<IUserController>();
            mockController.Setup(m => m.RegisterUser(It.IsAny<User>())).Returns(user);
            _userService.UserController = mockController.Object;
            var userDto = new UserDTO
            {
                Email = "zbi@g.com",
                FirstName = "Zaid",
                LastName = "Irfan",
                Password = "123456",
                DOB = new DateTime(1988, 10, 27),
                Gender = "M"
            };

            var res=_userService.Post(userDto);
            Assert.AreEqual("1278",res.Id);
        }

        [TestMethod]
        public void ShouldGetUserByUsernamePassword()
        {
            var user = this.CreateUserForCredential("zbi@g.com", "123456");
            var mockController = new Mock<IUserController>();
            mockController.Setup(m => m.GetUser(It.IsAny<string>(),It.IsAny<string>())).Returns(user);
            _userService.UserController = mockController.Object;
            var res = _userService.Get("zbi@g.com","123456");
            Assert.AreEqual(res.Id, "1278");
        }

        [TestMethod]
        public void ShouldChangePasswordForLoggedUser()
        {
            var user = this.CreateUserForCredential("zbi@g.com", "123456");
            var service = new UserService();
            service.ChangePassword("zbi@g.com","123456","abcdef");
            Assert.AreEqual(1,user.IsActive);
        }

        [TestMethod]
        public void ShouldChangePasswordForUnloggedUser()
        {
            var user = this.CreateUserForCredential("zbi@g.com", "123456");
            user.ChangePassword("123456","abcdef");
            Assert.AreEqual(1, user.IsActive);
        }

        public User CreateUserForCredential(string email, string password)
        {
            return new User
            {
                Email = email,
                FirstName = "Zaid",
                LastName = "Irfan",
                IsActive = 0,
                Password = password,
                DOB = new DateTime(1988, 10, 27),
                Gender = "M",
                ChangedPassword = password,
                Id = "1278",
                LastSeen = DateTime.Now,
                CreatedOn = DateTime.Now,
                Roles = new List<Role> { new Role { RoleId = 2 } }
            };
        }
    }

    public class ContainerSetup : IContainerSetup
    {
        public void Setup(Infrastructure.Container.IDependencyContainer container)
        {
            container.Register<IEventHandler<EntityUpdateEvent<User>>, MockUserEventHandler>("userupdatehandler")
                .Register<IDispatcher, Dispatcher>()
                .Register<IUnitOfWork,UnitOfWork>();
        }
    }


    public class MockUserEventHandler : IEventHandler<EntityUpdateEvent<User>>
    {
        public void Handle(EntityUpdateEvent<User> eventObject)
        {
            eventObject.Entity.IsActive = 1;
        }
    }

}
