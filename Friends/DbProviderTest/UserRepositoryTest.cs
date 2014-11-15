using System;
using System.Data.Entity;
using System.Linq;
using DAL;
using Infrastructure.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbProviderTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        UnitOfWork UOW { get; set; }
        UserRepository UserRepository { get; set; }
        public UserRepositoryTest()
        {
            UOW = new UnitOfWork();
            UserRepository = new UserRepository(UOW);
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
            var user = new User {Email = "zaid.b.irfan@gmail.com", IsActive = 1, Password = "qwerty",ChangedPassword = "qwerty",Username = Guid.NewGuid().ToString(),LastSeen = DateTime.Now,CreatedOn = DateTime.Now};
            user = UserRepository.AddUser(user);
            UOW.Commit();
            Assert.AreNotEqual(user.UserId,0);
            UserRepository.Delete(user);
            UOW.Commit();
        }
    }
}
