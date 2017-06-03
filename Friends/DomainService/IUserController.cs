using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using BusinessDomain.DomainObjects;
using Infrastructure.Model;

namespace DomainService
{
    public interface IUserController
    {
        User GetUser(string email, string password);
        User GetUserByEmail(string email);
        User GetUserById(string userId);
        User RegisterUser(User user);
        Profile GetProfile(string userId,User authUser);
        //Profile UpdateProfile(Profile profile, User authUser);
        IEnumerable<Profile> GetProfiles(IEnumerable<string> userIds, User authUser);
        IEnumerable<Profile> GetProfiles(SearchFilter filter, User authUser);
        //bool ChangePassword(User authUser,string oldPassword,string newPassword);
        //bool ChangeEmail(User authUser, string email);
    }
}
