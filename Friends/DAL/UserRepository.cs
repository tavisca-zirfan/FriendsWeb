using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using FriendsDb.Models;
using Infrastructure.Data;
using Infrastructure.Model;
using Role = FriendsDb.Models.Role;

namespace DAL
{
    public class UserRepository:EfBaseRepository<UserCredential>,IUserRepository
    {
        private FriendsContext Db;
        
        public UserRepository(IUnitOfWork uow):base(uow)
        {
            Db = Context as FriendsContext;
            
        } 

        public UserRepository()
        {
            Db = new FriendsContext();
        }
        public User GetUserByEmail(string emailId)
        {
            var user = (from ur in Db.UserRoles
                join r in Db.Roles on ur.RoleId equals r.RoleId
                group r by ur.UserId
                into roles
                join uc in Db.UserCredentials on roles.Key equals uc.UserId
                join up in Db.UserProfiles on uc.UserId equals up.UserId
                where uc.Email == emailId
                select new {Credential = uc, Profile = up, Roles = roles}).FirstOrDefault();
                       
            return user!=null ? user.Credential.ToBusinessModel(user.Profile, user.Roles) : null;
        }

        public User AddUser(User user)
        {
            try
            {
                var credential = new UserCredential();
                user.ToDbModel(credential);
                Db.UserCredentials.Add(credential);
                return credential.ToBusinessModel();
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
        }

        public Profile GetProfile(string userId)
        {
            using (var db = new FriendsContext())
            {
                var user = db.UserCredentials.Include("UserProfile").Where(uc => uc.UserId == userId)
                    .Select(uc=>uc).FirstOrDefault();
                return user==null?null: user.UserProfile.ToBusinessModel(user);
            }
        }

        public User GetUser(string userId)
        {
            var user = (from ur in Db.UserRoles
                        join r in Db.Roles on ur.RoleId equals r.RoleId
                        group r by ur.UserId
                            into roles
                            join uc in Db.UserCredentials on roles.Key equals uc.UserId
                            join up in Db.UserProfiles on uc.UserId equals up.UserId
                            where uc.UserId == userId
                            select new { Credential = uc, Profile = up, Roles = roles }).FirstOrDefault();
            return user != null ? user.Credential.ToBusinessModel(user.Profile, user.Roles) : null;
        }

        public bool CheckCredentialIfUserIdExist(string userId)
        {
            return Db.UserCredentials.Count(u => u.UserId == userId) > 0;
        }

        public bool CheckProfileIfUserIdExist(string userId)
        {
            return Db.UserProfiles.Count(u => u.UserId == userId) > 0;
        }

        public Profile AddProfile(string userId,Profile profile)
        {
            var userProfile = new UserProfile{UserId = userId};
            profile.ToDbModel(userProfile);
            Db.UserProfiles.Add(userProfile);
            return profile;
        }

        public void AddRoles(string userId, IEnumerable<int> roles)
        {
            IEnumerable<FriendsDb.Models.UserRole> userRoles = roles.Select(r => new FriendsDb.Models.UserRole {RoleId = r,UserId = userId});
            Db.UserRoles.AddRange(userRoles);
        }

        public void UpdateCredential(User user)
        {
            var credential = Db.UserCredentials.FirstOrDefault(uc => uc.UserId == user.UserId);
            user.ToDbModel(credential);
        }

        public void UpdateProfile(User user, Profile profile)
        {
            var userProfile = Db.UserProfiles.FirstOrDefault(up => up.UserId == user.UserId);
            profile.ToDbModel(userProfile);
        }

        public void RemoveRoles(User user, IEnumerable<int> roles)
        {
            Db.UserRoles.RemoveRange(roles.Select(r => new UserRole {RoleId = r, UserId = user.UserId}));
        }


        public void DeleteCredential(string userId)
        {
            var credential = Db.UserCredentials.FirstOrDefault(u => u.UserId == userId);
            Db.UserCredentials.Remove(credential);
        }

        public void DeleteProfile(string userId)
        {
            var profile = Db.UserProfiles.FirstOrDefault(u => u.UserId == userId);
            Db.UserProfiles.Remove(profile);
        }


        public List<Infrastructure.Model.Role> GetRoles(string userId)
        {
            var roles = (from ur in Db.UserRoles
                join r in Db.Roles on ur.RoleId equals r.RoleId
                where ur.UserId == userId
                select r).ToList();
            return roles.ToBusinessModel();
        }
    }
}
