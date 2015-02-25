using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace DbProviderTest
{
    public static class PostGenerator
    {
        public static Post CreatePost(string postId,string userId)
        {
            return new Post
            {
                Author = new User {Id = userId},
                CreatedAt = DateTime.Now,
                PostId = postId,
                PostType = PostType.Post,
                Recipient = new User {Id = userId},
                PostMessage = "Post"
            };
        }

        public static Comment CreateComment(string commentId,PostType postType,string userId)
        {
            return new Comment
            {
                Id = commentId,
                CommentedBy = new User {Id = userId},
                CommentMessage = "Comment",
                CommentedAt = DateTime.Now,
                PostType = postType
            };
        }
    }
}
