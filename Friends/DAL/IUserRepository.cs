using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Model;

namespace DAL
{
    public interface IUserRepository
    {
        User GetUser(string username);
        User GetUser(int userId);
        Profile GetProfile(string username);
        Profile GetProfile(int userId);
        User AddUser(User user);
        Profile AddProfile(int userId,Profile profile);
        void AddRoles(User user,IEnumerable<int> roles);
        void UpdateCredential(User user);
        void UpdateProfile(User user,Profile profile);
        void RemoveRoles(User user, IEnumerable<int> roles);
    }
}
