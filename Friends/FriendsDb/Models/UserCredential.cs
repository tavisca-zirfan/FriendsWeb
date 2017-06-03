using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class UserCredential
    {
        public UserCredential()
        {
            this.Comments = new List<Comment>();
            this.EventInviteds = new List<EventInvited>();
            this.Events = new List<Event>();
            this.Images = new List<Image>();
            this.Likes = new List<Like>();
            this.Posts = new List<Post>();
            this.UserRoles = new List<UserRole>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime LastSeen { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int IsActive { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EventInvited> EventInviteds { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
