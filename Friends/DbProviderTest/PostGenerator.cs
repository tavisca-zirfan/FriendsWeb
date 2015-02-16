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
                Author = new User {UserId = userId},
                CreatedAt = DateTime.Now,
                PostId = postId,
                PostType = PostType.Post,
                Recipient = new User {UserId = userId},
                PostMessage = "Post"
            };
        }

        public static Comment CreateComment(string commentId,PostType postType,string userId)
        {
            return new Comment
            {
                CommentId = commentId,
                CommentedBy = new User {UserId = userId},
                CommentMessage = "Comment",
                CommentedAt = DateTime.Now,
                PostType = postType
            };
        }
    }
}
