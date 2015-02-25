using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Model;
using BusinessDomain.DomainObjects;

namespace DAL
{
    public static class UserTranslator
    {
        public static User ToBusinessModel(this FriendsDb.Models.UserCredential credential, UserProfile profile=null,
            IEnumerable<FriendsDb.Models.Role> roles=null)
        {
            var user = new User
            {
                Id = credential.UserId,
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

        public static BusinessDomain.DomainObjects.Role ToBusinessModel(this FriendsDb.Models.Role role)
        {
            return new BusinessDomain.DomainObjects.Role
            {
                RoleId = role.RoleId,RoleName = role.RoleName
            };
        }

        public static List<BusinessDomain.DomainObjects.Role> ToBusinessModel(this List<FriendsDb.Models.Role> role)
        {
            var roles = new List<BusinessDomain.DomainObjects.Role>();
            role.ForEach(r=>roles.Add(r.ToBusinessModel()));
            return roles;
        }

        public static void ToDbModel(this User user,UserCredential credential)
        {
            credential.Email = user.Email;
            credential.IsActive = user.IsActive;
            credential.LastSeen = user.LastSeen;
            credential.CreatedOn = user.CreatedOn;
            if (!string.IsNullOrEmpty(user.ChangedPassword))
            {
                if(credential.Password.Trim()!=user.Password.Trim())
                    throw new Exception("Credentials mismatch");
                credential.Password = user.ChangedPassword;
            }
            credential.UserId = user.Id;
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

        public static Profile ToProfile(this User user)
        {
            return new Profile{DOB = user.DOB,Email = user.Email,FirstName=user.FirstName,LastName = user.LastName,Gender=user.Gender,LastSeen = user.LastSeen};
        }

        public static void AddProfileInformation(this User user,Profile profile)
        {
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Gender = profile.Gender;
            user.DOB = profile.DOB;
        }
    }
}
