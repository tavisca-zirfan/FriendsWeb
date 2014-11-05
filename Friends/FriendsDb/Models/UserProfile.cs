using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
