using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace DbProviderTest
{
    public class UserGenerator
    {
        public static User CreateUserForCredential(string email,string password)
        {
            return new User { Email = email, IsActive = 1, Password = password, ChangedPassword = password
                , UserId = Guid.NewGuid().ToString(), LastSeen = DateTime.Now, CreatedOn = DateTime.Now, Roles = new List<Role>{new Role{RoleId = 2}}};
        }

        public static Profile CreateProfile()
        {
            return new Profile { DOB = new DateTime(1988, 10, 27),FirstName = "John",Gender = "M",LastName = "Doe"};
        }
    }
}
