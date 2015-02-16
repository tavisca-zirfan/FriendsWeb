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
            var user = UserRepository.GetUser(username);
            if (user!=null && user.Password == password)
                return user;
            else
            {
                return null;
            }
        }

        public User RegisterUser(User user, Profile profile)
        {
            user.UserId = new RainDrop().GetNextId();
            user.LastSeen = DateTime.Now;
            user.CreatedOn = DateTime.Now;
            UserRepository.AddUser(user,profile);
            UserRepository.AddRoles(user.UserId,new List<int>{2});
            UnitOfWork.Commit();
            return user;
        }

        public Profile GetProfile(string userId)
        {
            return UserRepository.GetProfile(userId);
        }

        public Profile UpdateProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userId,string oldPassword, string newPassword)
        {
            var user = new User{UserId = userId,Password=oldPassword,ChangedPassword = newPassword};
            user.ChangedPassword = newPassword;
            UserRepository.UpdateCredential(user);
            return true;
        }

        public bool ChangeEmail(string userId, string email)
        {
            throw new NotImplementedException();
        }
        
        public Profile UpdateProfile(string userId, Profile profile)
        {
            UserRepository.UpdateProfile(userId,profile);
            return profile;
        }
    }
}
