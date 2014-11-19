using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using FriendsDb.Models;
using Infrastructure.Data;
using Infrastructure.Model;

namespace BLL
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
            
            user.UserId = Guid.NewGuid().ToString();
            user.LastSeen = DateTime.Now;
            user.CreatedOn = DateTime.Now;
            user.ChangedPassword = user.Password;
            UserRepository.AddUser(user);
            UserRepository.AddProfile(user.UserId,profile);
            UserRepository.AddRoles(user.UserId,new List<int>{2});
            UnitOfWork.Commit();
            return user;
        }

        public Profile GetProfile(string userId)
        {
            return UserRepository.GetProfile(userId);
        }

        
    }
}
