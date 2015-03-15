using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Model;
using BusinessDomain.DomainObjects;

namespace DAL
{
    public interface IUserRepository
    {
        User GetUserByEmail(string emailId);
        User GetUser(string userId);
        Profile GetProfile(string userId);
        IEnumerable<Profile> GetProfiles(IEnumerable<string> userIds);
        IEnumerable<Profile> GetProfiles(SearchFilter filter);
        User AddUser(User user);
        void AddRoles(string userId,IEnumerable<int> roles);
        List<BusinessDomain.DomainObjects.Role> GetRoles(string userId); 
        void UpdateCredential(User user);
        void UpdateProfile(Profile profile);
        void RemoveRoles(string userId, IEnumerable<int> roles);
        void DeleteCredential(string userId);
        IEnumerable<Profile> GetFriends(string userId);
    }
}
