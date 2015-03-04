using System;
using System.Collections.Generic;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainObjects;
using DomainService;
using Infrastructure.Configuration;
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
            _userService.UserId = "1278";
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
            Assert.AreEqual(user.Id,"1278");
        }

        [TestMethod]
        public void ShouldGetUserByUsernamePassword()
        {
        }

        [TestMethod]
        public void ShouldChangePasswordForLoggedUser()
        {
        }

        [TestMethod]
        public void ShouldChangePasswordForUnloggedUser()
        {
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
                .Register<IDispatcher, Dispatcher>();
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
