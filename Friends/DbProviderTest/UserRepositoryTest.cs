using System;
using System.Collections.Generic;
using System.Linq;
using BusinessDomain;
using DAL;
using BusinessDomain.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbProviderTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        private UnitOfWork UOW { get; set; }
        private IUserRepository UserRepository { get; set; }
        private const string Email = "test@testmail.com";
        private const string Password = "qwerty";

        private User CreateUser()
        {
            var user = UserGenerator.CreateUserForCredential(Email, Password);
            var profile = UserGenerator.CreateProfile();
            UserRepository.AddUser(user);
            //UserRepository.AddProfile(user.UserId, profile);
            UserRepository.AddRoles(user.Id, new List<int> {1, 2});
            UOW.Commit();
            user.AddProfileInformation(profile);
            return user;
        }

        private void DeleteUser(string userId)
        {
            UserRepository.DeleteCredential(userId);
            UOW.Commit();
        }

        public UserRepositoryTest()
        {
            UOW = new UnitOfWork();
            UserRepository = new UserRepository(UOW);
        }

        [TestMethod]
        public void GetUserTest()
        {
            var nuser = CreateUser();
            var user = UserRepository.GetUserByEmail(Email);
            Assert.IsNotNull(user);
            Assert.AreEqual(2, user.Roles.Count());
            DeleteUser(nuser.Id);
        }

        [TestMethod]
        public void AddUser()
        {
            //var user = UserGenerator.CreateUserForCredential(Email, Password);
            //user = UserRepository.AddUser(user,profile:);
            //UOW.Commit();
            //var isUserSaved = UserRepository.CheckCredentialIfUserIdExist(user.UserId);
            //Assert.IsTrue(isUserSaved);
            //UserRepository.DeleteCredential(user.UserId);
            //UOW.Commit();
        }

        [TestMethod]
        public void AddUserProfile()
        {
            var user = UserGenerator.CreateUserForCredential(Email, Password);
            var profile = UserGenerator.CreateProfile();
            user = UserRepository.AddUser(user);
            //UserRepository.AddProfile(user.UserId, profile);
            UOW.Commit();
            var isUserSaved = UserRepository.GetUser(user.Id)!=null;
            Assert.IsTrue(isUserSaved);
            profile = UserRepository.GetProfile(user.Id);
            Assert.IsNotNull(profile);
            UserRepository.DeleteCredential(user.Id);
            UOW.Commit();
        }

        [TestMethod]
        public void AddUserProfileIfCredentialDoesNotExist()
        {
            //try
            //{
            //    var profile = UserGenerator.CreateProfile();
            //    profile = UserRepository.AddProfile("invalidid", profile);
            //    UOW.Commit();
            //}
            //catch (Exception ex)
            //{
            //    Assert.IsNotNull(ex);
            //}
            //var myProfile = UserRepository.GetProfile("invalidid");
            //Assert.IsNull(myProfile);
        }

        [TestMethod]
        public void AddUserRoles()
        {
            var user = UserGenerator.CreateUserForCredential(Email, Password);
            user = UserRepository.AddUser(user);
            UserRepository.AddRoles(user.Id, new List<int> {1, 2});
            UOW.Commit();
            var roles = UserRepository.GetRoles(user.Id);
            Assert.AreEqual(roles.Count, 2);
            UserRepository.DeleteCredential(user.Id);
            UOW.Commit();
        }

        [TestMethod]
        public void AddUserRoleIfCredentialDoesNotExist()
        {
            try
            {
                UserRepository.AddRoles("invalidid", new List<int> {1, 2});
                UOW.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }

        }

        [TestMethod]
        public void UpdateUserCredential()
        {

            var user = CreateUser();
            user.ChangedPassword = "newpassword";
            user.Email = "newemail";
            UserRepository.UpdateCredential(user);
            UOW.Commit();
            var modifiedUser = UserRepository.GetUser(user.Id);
            Assert.AreEqual(modifiedUser.Email, "newemail");
            Assert.AreEqual(modifiedUser.Password, "newpassword");
            UserRepository.DeleteCredential(modifiedUser.Id);
            UOW.Commit();

        }

        [TestMethod]
        public void UpdateUserCredentialShouldFailIfUserDoesNotExist()
        {
            var user = new User {Id = "invaliduser", ChangedPassword = "newpassword", Email = "newemail"};
            try
            {
                UserRepository.UpdateCredential(user);
                UOW.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }

        }

        [TestMethod]
        public void UpdateUserProfile()
        {

            var user = CreateUser();
            var profile = user.ToProfile();
            profile.FirstName = "ChangedFirstName";
            profile.LastName = "ChangedLastName";
            UserRepository.UpdateProfile(profile);
            UOW.Commit();
            var modifiedProfile = UserRepository.GetProfile(user.Id);
            Assert.AreEqual(modifiedProfile.FirstName, "ChangedFirstName");
            Assert.AreEqual(modifiedProfile.LastName, "ChangedLastName");
            UserRepository.DeleteCredential(user.Id);
            UOW.Commit();
        }

        [TestMethod]
        public void UpdateUserProfileShouldFailIfUserDoesNotExist()
        {
            var profile = new Profile {FirstName = "invaliduser", LastName = "invaliduser", Email = "newemail",Id="invaliduser"};
            try
            {
                UserRepository.UpdateProfile( profile);
                UOW.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void RemoveUserRoles()
        {
            var user = CreateUser();
            UserRepository.RemoveRoles(user.Id, new List<int> {1});
            UOW.Commit();
            var roles = UserRepository.GetRoles(user.Id);
            Assert.AreEqual(roles.Count, 1);
            Assert.AreEqual(roles[0].RoleName, "User");
            UserRepository.DeleteCredential(user.Id);
            UOW.Commit();
        }

        [TestMethod]
        public void RemoveUserRolesIfUserDoesNotExistShouldFail()
        {
            try
            {
                UserRepository.RemoveRoles("invaliduser", new List<int> {1});
                UOW.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void DeleteUser()
        {

        }

        [TestMethod]
        public void DeleteUserCredentialIfDoesNotShouldFail()
        {

        }

    }
}
