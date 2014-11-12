using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using FriendsDb.Models;
using Infrastructure.Data;
using Infrastructure.Model;
using Role = FriendsDb.Models.Role;

namespace DAL
{
    public class UserRepository:EfBaseRepository<User>,IUserRepository
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
        public User GetUser(string username)
        {
            var user = (from ur in Db.UserRoles
                join r in Db.Roles on ur.RoleId equals r.RoleId
                group r by ur.UserId
                into roles
                join uc in Db.UserCredentials on roles.Key equals uc.UserId
                join up in Db.UserProfiles on uc.UserId equals up.UserId
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
                var savedUser = credential.ToBusinessModel();
                return savedUser;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
        }

        public Profile GetProfile(int userId)
        {
            using (var db = new FriendsContext())
            {
                var user = db.UserCredentials.Include("UserProfile").Where(uc => uc.UserId == userId)
                    .Select(uc=>uc).FirstOrDefault();
                return user==null?null: user.UserProfile.ToBusinessModel(user);
            }
        }
        public Profile GetProfile(string username)
        {
            using (var db = new FriendsContext())
            {
                var user = db.UserCredentials.Include("UserProfile").Where(uc => uc.Username == username)
                    .Select(uc => uc).FirstOrDefault();
                return user == null ? null : user.UserProfile.ToBusinessModel(user);
            }
        }

        public User GetUser(int userId)
        {
            var user = from uc in Db.UserCredentials
                       join up in Db.UserProfiles on uc.UserId equals up.UserId
                       join r in Db.UserRoles on uc.UserId equals r.UserId
                       where uc.UserId==userId
                       select uc.ToBusinessModel(up, Db.Roles.Where(rn => rn.RoleId == r.RoleId)
                   );
            return user.FirstOrDefault();
        }


        public Profile AddProfile(int userId,Profile profile)
        {
            var userProfile = new UserProfile{UserId = userId};
            profile.ToDbModel(userProfile);
            Db.UserProfiles.Add(userProfile);
            return profile;
        }

        public void AddRoles(User user, IEnumerable<int> roles)
        {
            IEnumerable<FriendsDb.Models.UserRole> userRoles = roles.Select(r => new FriendsDb.Models.UserRole {RoleId = r,UserId = user.UserId});
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
    }
}
