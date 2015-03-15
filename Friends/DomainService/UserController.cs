using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using FriendsDb.Models;
using Infrastructure.Common;
using Infrastructure.Data;
using BusinessDomain.DomainObjects;

namespace DomainService
{
    public class UserController:IUserController
    {
        public IUserRepository UserRepository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public UserController()
        {
            UnitOfWork = new UnitOfWork();
            UserRepository = new UserRepository(UnitOfWork);
        }
        public User GetUser(string username, string password)
        {
            var user = GetUserByEmail(username);
            if (user!=null && user.Password == password)
                return user;
            else
            {
                return null;
            }
        }


        public User RegisterUser(User user)
        {
            user.Id = new RainDrop().GetNextId();
            user.LastSeen = DateTime.Now;
            user.CreatedOn = DateTime.Now;
            UserRepository.AddUser(user);
            UserRepository.AddRoles(user.Id,new List<int>{2});
            UnitOfWork.Commit();
            return user;
        }

        public Profile GetProfile(string userId, User authUser)
        {
            return UserRepository.GetProfile(userId);
        }

        public Profile UpdateProfile(Profile profile, User authUser)
        {
            UserRepository.UpdateProfile(profile);
            UnitOfWork.Commit();
            return profile;
        }

        public bool ChangePassword(User authUser,string oldPassword, string newPassword)
        {
            var user = new User{Id = authUser.Id,Password=oldPassword,ChangedPassword = newPassword};
            user.ChangedPassword = newPassword;
            UserRepository.UpdateCredential(user);
            UnitOfWork.Commit();
            return true;
        }

        public bool ChangeEmail(User authUser, string email)
        {
            throw new NotImplementedException();
        }




        public User GetUserByEmail(string email)
        {
            return UserRepository.GetUserByEmail(email);
        }

        public User GetUserById(string userId)
        {
            return UserRepository.GetUser(userId);
        }


        public IEnumerable<Profile> GetProfiles(IEnumerable<string> userIds, User authUser)
        {
            return UserRepository.GetProfiles(userIds);
        }


        public IEnumerable<Profile> GetProfiles(Infrastructure.Model.SearchFilter filter, User authUser)
        {
            throw new NotImplementedException();
        }
    }
}
