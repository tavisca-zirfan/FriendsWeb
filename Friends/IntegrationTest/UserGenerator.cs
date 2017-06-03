using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceLayer.Model;

namespace IntegrationTest
{
    public class UserGenerator
    {
        public static UserDTO CreateUserForCredential(string email, string password)
        {
            return new UserDTO
            {
                Email = email,
                Password = password,
                Roles = new List<RolesDTO> { new RolesDTO { Id = 2,RoleName="user" } },
                DOB = new DateTime(1988, 10, 27),
                FirstName = "John",
                Gender = "M",
                LastName = "Doe",
                Username = email
            };
        }

        
    }
}
