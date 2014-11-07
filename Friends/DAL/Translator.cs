using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Model;

namespace DAL
{
    public static class Translator
    {
        public static User ToBusinessModel(this FriendsDb.Models.UserCredential credential, UserProfile profile=null,
            IEnumerable<FriendsDb.Models.Role> roles=null)
        {
            var user = new User
            {
                UserId = credential.UserId,
                Username = credential.Username,
                Email = credential.Email,
                Password = credential.Email,
                IsActive = credential.IsActive
            };
            if (profile != null)
            {
                user.FirstName = profile.FirstName;
                user.LastName = profile.LastName;
                user.Gender = profile.Gender;
            }
            if (roles != null)
            {
                user.Roles = roles.Select(r => r.ToBusinessModel());
            }
            return user;
        }

        public static Infrastructure.Model.Role ToBusinessModel(this FriendsDb.Models.Role role)
        {
            return new Infrastructure.Model.Role
            {
                RoleId = role.RoleId,RoleName = role.RoleName
            };
        }

        public static void ToDbModel(this User user,UserCredential credential)
        {
            credential.Email = user.Email;
            credential.IsActive = user.IsActive;
            credential.Password = user.Password;
            credential.Username = user.Username;
        }
        public static void ToDbModel(this User user, UserProfile profile)
        {
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.Gender = user.Gender;
            profile.DOB = user.DOB;
        }
        
    }
}
