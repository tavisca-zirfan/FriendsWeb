using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace BusinessDomain
{
    public static class Translator
    {
        public static Profile ToProfile(this User user)
        {
            return new Profile
            {
                Id = user.Id,
                DOB = user.DOB,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                LastSeen = user.LastSeen,
                ImageUrl = user.ImageUrl
            };
        }

        public static void AddProfileInformation(this User user, Profile profile)
        {
            user.FirstName = profile.FirstName;
            user.LastName = profile.LastName;
            user.Gender = profile.Gender;
            user.DOB = profile.DOB;
            user.ImageUrl = profile.ImageUrl;
        }
    }
}
