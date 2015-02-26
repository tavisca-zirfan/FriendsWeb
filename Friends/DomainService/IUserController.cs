using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using BusinessDomain.DomainObjects;

namespace DomainService
{
    public interface IUserController
    {
        User GetUser(string email, string password);
        User RegisterUser(User user);
        Profile GetProfile(string userId);
        Profile UpdateProfile(Profile profile);
        bool ChangePassword(string userId,string oldPassword,string newPassword);
        bool ChangeEmail(string userId, string email);
    }
}
