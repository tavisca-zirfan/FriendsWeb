using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Model
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<RolesDTO> Roles { get; set; }
    }
}
