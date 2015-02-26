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
using BusinessDomain.DomainObjects;
using Role = FriendsDb.Models.Role;

namespace DAL
{
    public class UserRepository:EfBaseRepository<UserCredential>,IUserRepository
    {
        private FriendsContext Db;
        public IUnitOfWork UnitOfWork { get; set; }
        public UserRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            UnitOfWork = unitOfWork;
            if (unitOfWork == null)
                Db = new FriendsContext();
            else
            {
                Db = UnitOfWork.GetTransactionObject() as FriendsContext;
            }
            
        } 

        public UserRepository()
        {
            Db = new FriendsContext();
        }

        public User GetUserByEmail(string emailId)
        {
            using (var db = new FriendsContext())
            {
                var user = (from ur in db.UserRoles
                    join r in db.Roles on ur.RoleId equals r.RoleId
                    group r by ur.UserId
                    into roles
                    join uc in db.UserCredentials on roles.Key equals uc.UserId
                    join up in db.UserProfiles on uc.UserId equals up.UserId
                    where uc.Email == emailId
                    select new {Credential = uc, Profile = up, Roles = roles}).FirstOrDefault();


                return user != null ? user.Credential.ToBusinessModel(user.Profile, user.Roles) : null;
            }
        }

        public User AddUser(User user)
        {

           // using (var Db = new FriendsContext())
            {
            try
            {
                var credential = new UserCredential();
                user.ToDbModel(credential);
                Db.UserCredentials.Add(credential);
                    var userProfile = new UserProfile { UserId = credential.UserId };
                    user.ToDbModel(userProfile);
                    Db.UserProfiles.Add(userProfile);
                    if(user.Roles!=null)
                        AddRoles(credential.UserId,user.Roles.Select(r=>r.RoleId));
                    return user;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
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
            using (var db = new FriendsContext())
            {
                var user = (from ur in db.UserRoles
                            join r in db.Roles on ur.RoleId equals r.RoleId
                        group r by ur.UserId
                            into roles
                                join uc in db.UserCredentials on roles.Key equals uc.UserId
                                join up in db.UserProfiles on uc.UserId equals up.UserId
                                where uc.UserId == userId
                            select new { Credential = uc, Profile = up, Roles = roles }).FirstOrDefault();
            return user != null ? user.Credential.ToBusinessModel(user.Profile, user.Roles) : null;
        }
        }

        public bool CheckCredentialIfUserIdExist(string userId)
        {
            using (var db = new FriendsContext())
            {
                return db.UserCredentials.Count(u => u.UserId == userId) > 0; 
            }
        }

        public bool CheckProfileIfUserIdExist(string userId)
        {
            using (var db =new FriendsContext())
            {
                return db.UserProfiles.Count(u => u.UserId == userId) > 0; 
            }
        }

        public void AddRoles(string userId, IEnumerable<int> roles)
        {
            IEnumerable<FriendsDb.Models.UserRole> userRoles = roles.Select(r => new FriendsDb.Models.UserRole {RoleId = r,UserId = userId});
            Db.UserRoles.AddRange(userRoles);
        }

        public void UpdateCredential(User user)
        {
            var credential = Db.UserCredentials.FirstOrDefault(uc => uc.UserId == user.Id);
            if(credential == null)
                throw new Exception("User Not Found");
            user.ToDbModel(credential);
        }

        public void UpdateProfile(Profile profile)
        {
            var userProfile = Db.UserProfiles.FirstOrDefault(up => up.UserId == profile.Id);
            if (userProfile == null)
                throw new Exception("User Not Found");
            profile.ToDbModel(userProfile);
        }

        public void RemoveRoles(string userId, IEnumerable<int> roles)
        {
            Db.UserRoles.RemoveRange(Db.UserRoles.Where(ur=>ur.UserId==userId&& roles.Contains(ur.RoleId)));
        }


        public void DeleteCredential(string userId)
        {
            var credential = Db.UserCredentials.FirstOrDefault(u => u.UserId == userId);
            if (credential == null)
                //throw new Exception("User Not Found");
                return;
            Db.UserCredentials.Remove(credential);
            var profile = Db.UserProfiles.FirstOrDefault(u => u.UserId == userId);
            //if (profile == null)
                //throw new Exception("User Not Found");
            Db.UserProfiles.Remove(profile);
            Db.UserRoles.RemoveRange(Db.UserRoles.Where(ur => ur.UserId == userId));
        }

        public List<BusinessDomain.DomainObjects.Role> GetRoles(string userId)
        {
            var roles = (from ur in Db.UserRoles
                join r in Db.Roles on ur.RoleId equals r.RoleId
                where ur.UserId == userId
                select r).ToList();
            return roles.ToBusinessModel();
        }
    }
}
