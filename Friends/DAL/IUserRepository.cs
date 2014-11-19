﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Model;

namespace DAL
{
    public interface IUserRepository
    {
        User GetUserByEmail(string emailId);
        User GetUser(string userId);
        Profile GetProfile(string userId);
        User AddUser(User user);
        Profile AddProfile(string userId,Profile profile);
        void AddRoles(string userId,IEnumerable<int> roles);
        List<Infrastructure.Model.Role> GetRoles(string userId); 
        void UpdateCredential(User user);
        void UpdateProfile(User user,Profile profile);
        void RemoveRoles(User user, IEnumerable<int> roles);
        void DeleteCredential(string userId);
        void DeleteProfile(string userId);
        bool CheckCredentialIfUserIdExist(string userId);
        
    }
}
