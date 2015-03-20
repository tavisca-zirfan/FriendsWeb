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
            _user = _userService.Get(_userId);
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

        [TestMethod]
        public void ChangePassword()
        {

            _userService.ChangePassword(_user, "123456", "zaq1ZAQ!");
            var user = _userService.Get("test.user@test.com", "zaq1ZAQ!");
            Assert.IsNotNull(user);
            Assert.AreEqual(_userId, user.Id);
            _userService.ChangePassword(user, "zaq1ZAQ!", "123456");

        }

        [TestMethod]
        public void ChangePasswordForUnauthorizedUser()
        {

            _userService.ChangePassword("test.user@test.com", "123456", "zaq1ZAQ!");
            var user = _userService.Get("test.user@test.com", "zaq1ZAQ!");
            Assert.IsNotNull(user);
            Assert.AreEqual(_userId, user.Id);
            _userService.ChangePassword(user, "zaq1ZAQ!", "123456");

        }
    }
}
