using System;
using BusinessDomain.DomainEvents.Common;
using Infrastructure.Common;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class Comment:Post
    {
        public string CommentMessage { get; set; }
        public string  ForPostId { get; set; }

        public Comment()
        {
            PostType=PostType.Comment;
        }
    }
}
