using System;
using DbProviderTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.Model;

namespace IntegrationTest
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _userService;
        private string _userId = "ns82ugv6uwx";
        private UserDTO _user;
        public UserServiceTest()
        {
            _userService=new UserService();
            ServiceModelMapper.CreateMap();
        }

        [TestMethod]
        public void CreateUser()
        {

            var user = UserGenerator.CreateUserForCredential("test.user@test.com", "123456");
            var userdto = _userService.Post(user);
            Assert.IsNotNull(userdto);

        }

        [TestMethod]
        public void GetUser()
        {
            var user = _userService.Get("test.user@test.com", "123456");
            Assert.IsNotNull(user);
            Assert.AreEqual(_userId,user.Id);
        }
    }
}
