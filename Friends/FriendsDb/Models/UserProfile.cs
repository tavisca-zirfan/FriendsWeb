using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Likes = new List<Like>();
            this.Posts = new List<Post>();
            this.PostRecipients = new List<PostRecipient>();
            this.PostTags = new List<PostTag>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostRecipient> PostRecipients { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
