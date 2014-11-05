using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FriendsDb.Models;
using Infrastructure.Model;

namespace DAL
{
    public class UserRepository:IUserRepository
    {
        public User GetUser(string username, string password)
        {

            using (var db = new FriendsContext())
            {
                return db.UserCredentials.Include("UserProfile").Include("Roles").Where(uc => uc.Email == username && uc.Password == password)
                    .Select(uc => new User
                    {
                        UserId = uc.UserId,Username = uc.Username,Email = uc.Email,FirstName = uc.UserProfile.FirstName,
                        Gender=uc.UserProfile.Gender,LastName = uc.UserProfile.LastName,
                        Roles = uc.UserRoles.Select(r=>new Infrastructure.Model.Role{RoleId = r.RoleId,RoleName = r.Role.RoleName}).ToList(),
                    }).FirstOrDefault();
            }
            
        }

        public bool RegisterUser(FriendsDb.Models.UserCredential credentials, FriendsDb.Models.UserProfile profile,IEnumerable<int> roles)
        {
            using (var db = new FriendsContext())
            {
                var response = db.UserCredentials.Add(credentials);
                profile.UserId = response.UserId;
                var responseProfile = db.UserProfiles.Add(profile);
                var userRoles = roles.Select(r => new UserRole {RoleId = r, UserId = response.UserId});
                db.UserRoles.AddRange(userRoles);
                return db.SaveChanges()>0;
            }
        }

        public Profile GetProfile(int userId)
        {
            using (var db = new FriendsContext())
            {
                return db.UserCredentials.Include("UserProfile").Where(uc => uc.UserId == userId)
                    .Select(uc => new Profile
                    {
                        Email = uc.Email,
                        FirstName = uc.UserProfile.FirstName,
                        Gender = uc.UserProfile.Gender,
                        LastName = uc.UserProfile.LastName,
                        About = uc.UserProfile.About,
                        DOB = uc.UserProfile.DOB,
                        LastSeen = uc.LastSeen,
                        Location = ""
                    }).First();
            }
        }
        public Profile GetProfile(string username)
        {
            using (var db = new FriendsContext())
            {
                return db.UserCredentials.Include("UserProfile").Where(uc => uc.Username == username)
                    .Select(uc => new Profile
                    {
                        Email = uc.Email,
                        FirstName = uc.UserProfile.FirstName,
                        Gender = uc.UserProfile.Gender,
                        LastName = uc.UserProfile.LastName,
                        About = uc.UserProfile.About,
                        DOB = uc.UserProfile.DOB,
                        LastSeen = uc.LastSeen,
                        Location = ""
                    }).First();
            }
        }
    }
}
