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
                Password = credential.Password,
                IsActive = credential.IsActive,
                LastSeen = credential.LastSeen
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
            credential.LastSeen = user.LastSeen;
            credential.CreatedOn = user.CreatedOn;
            if (!string.IsNullOrEmpty(user.ChangedPassword))
            {
                credential.Password = user.ChangedPassword;
            }
            credential.Username = user.Username;
        }
        public static void ToDbModel(this User user, UserProfile profile)
        {
            profile.FirstName = user.FirstName;
            profile.LastName = user.LastName;
            profile.Gender = user.Gender;
            profile.DOB = user.DOB;
        }
        public static Profile ToBusinessModel(this UserProfile userProfile,UserCredential user=null)
        {
            var profile = new Profile
            {
                About = userProfile.About,
                DOB = userProfile.DOB,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                Gender = userProfile.Gender
            };

            if (user != null)
            {
                profile.Email = user.Email;
            }
            return profile;
        }
        public static void ToDbModel(this Profile profile, UserProfile userProfile)
        {
            userProfile.About = profile.About;
            userProfile.DOB = profile.DOB;
            userProfile.FirstName = profile.FirstName;
            userProfile.LastName = profile.LastName;
            userProfile.Gender = profile.Gender;
        }
    }
}
