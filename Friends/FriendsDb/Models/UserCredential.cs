using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class UserCredential
    {
        public UserCredential()
        {
            this.UserRoles = new List<UserRole>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime LastSeen { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int IsActive { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
