using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using ServiceLayer;

namespace Friends.Classes
{
    public class CustomPrincipal:IPrincipal
    {
        IUserService service = new MockUserService();
        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }
        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Any(r => r == role);
        }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] Roles { get; set; }
    }
}