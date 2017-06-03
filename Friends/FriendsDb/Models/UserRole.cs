using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
