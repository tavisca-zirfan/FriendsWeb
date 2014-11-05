using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using FriendsDb.Models;
using Infrastructure.Model;

namespace BLL
{
    public class UserController:IUserController
    {
        IUserRepository UserRepository { get; set; }

        public UserController()
        {
            UserRepository = new UserRepository();
        }
        public User GetUser(string username, string password)
        {
            return UserRepository.GetUser(username, password);
        }

        public User RegisterUser(User user, Profile profile)
        {

            var credential = new UserCredential
            {
                Email = user.Email,
                CreatedOn = DateTime.Now,
                IsActive = 1,
                LastSeen = DateTime.Now,
                Password = user.Password,
                Username = Guid.NewGuid().ToString()
            };
            var userProfile = new UserProfile
            {
                DOB = profile.DOB,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Gender = profile.Gender,
                LocationId = profile.LocationId,
                StatusId = profile.StatusId
            };
            if (UserRepository.RegisterUser(credential, userProfile,user.Roles.Select(r=>r.RoleId)))
            {
                user.Username = credential.Username;
                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Gender = profile.Gender;
                return user;
            }
            return null;
        }

        public Profile GetProfile(int userId)
        {
           return  UserRepository.GetProfile(userId);
        }

        public Profile GetProfile(string username)
        {
            return UserRepository.GetProfile(username);
        }
    }
}
