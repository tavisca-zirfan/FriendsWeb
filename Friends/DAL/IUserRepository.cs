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
        bool RegisterUser(UserCredential credentials, UserProfile profile,IEnumerable<int>roles);
        Profile GetProfile(int userId);
        Profile GetProfile(string username);
    }
}
