using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class PostTag
    {
        public int Id { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public virtual Post Post { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
