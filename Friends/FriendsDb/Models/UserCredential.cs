using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class UserCredential
    {
        public UserCredential()
        {
            this.EventInviteds = new List<EventInvited>();
            this.Images = new List<Image>();
            this.Likes = new List<Like>();
            this.UserRoles = new List<UserRole>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime LastSeen { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int IsActive { get; set; }
        public virtual ICollection<EventInvited> EventInviteds { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
