using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string CommentId { get; set; }
        public string TypeId { get; set; }
        public string Comment1 { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> CommentTime { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
