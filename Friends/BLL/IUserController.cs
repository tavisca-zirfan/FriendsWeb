using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Model;

namespace BLL
{
    public interface IUserController
    {
        User GetUser(string email, string password);
        User RegisterUser(User credentials, Profile profile);
        Profile GetProfile(string userId);
    }
}
