using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Post
    {
        public Post()
        {
            this.Likes = new List<Like>();
            this.PostRecipients = new List<PostRecipient>();
            this.PostTags = new List<PostTag>();
        }

        public int Id { get; set; }
        public string Pid { get; set; }
        public string Author { get; set; }
        public System.DateTime Time { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual Event Event { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<PostRecipient> PostRecipients { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual PostText PostText { get; set; }
    }
}
