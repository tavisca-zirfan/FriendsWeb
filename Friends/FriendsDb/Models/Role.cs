using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Role
    {
        public Role()
        {
            this.UserRoles = new List<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
