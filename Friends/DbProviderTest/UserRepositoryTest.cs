using System;
using System.Linq;
using DAL;
using Infrastructure.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbProviderTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        UserRepository UserRepository { get; set; }
        public UserRepositoryTest()
        {
            var uow = new UnitOfWork();
            UserRepository = new UserRepository(uow);
        }
        [TestMethod]
        public void GetUserTest()
        {
            var username = "zaid.irfan@gmail.com";
            var uow = new UnitOfWork();
            var repo = new UserRepository(uow);
            var user = repo.GetUser(username);
            Assert.IsNotNull(user);
            Assert.AreEqual(2,user.Roles.Count());
        }

        [TestMethod]
        public void AddUser()
        {
            var user = new User {Email = "zaid.b.irfan@gmail.com", IsActive = 1, Password = "qwerty",ChangedPassword = "qwerty",Username = Guid.NewGuid().ToString(),LastSeen = DateTime.Now};
            user = UserRepository.AddUser(user);
            Assert.IsNotNull(user);
        }
    }
}
