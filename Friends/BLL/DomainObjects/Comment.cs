using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessDomain.DomainObjects
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string CommentMessage { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime? CommentedAt { get; set; }
        public User CommentedBy { get; set; }
        public PostType? PostType { get; set; }
    }
}
