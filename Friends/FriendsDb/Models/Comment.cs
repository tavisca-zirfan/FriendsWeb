using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string CommentId { get; set; }
        public string ForPostId { get; set; }
        public string CommentMessage { get; set; }
        public virtual Post Post { get; set; }
    }
}
