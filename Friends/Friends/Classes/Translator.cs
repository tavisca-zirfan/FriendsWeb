using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceLayer.Model;

namespace Friends.Classes
{
    public static class Translator
    {
        public static ProfileDTO ToProfile(this UserDTO user)
        {
            var profile = new ProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                DOB = user.DOB,
                Id = user.Id,
            };
            return profile;
        }
    }
}