using System;
using BusinessDomain.DomainEvents.Common;
using Infrastructure.Common;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class Comment:Post
    {
        public string CommentMessage { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime? CommentedAt { get; set; }
        public User CommentedBy { get; set; }
        public PostType? PostType { get; set; }
        public string  PostId { get; set; }
    }
}
