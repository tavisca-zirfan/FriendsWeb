using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace DbProviderTest
{
    public static class PostGenerator
    {
        public static Post CreateTextPost(string postId,string userId)
        {
            return new TextPost
            {
                Author = new Profile {Id = userId},
                CreatedAt = DateTime.Now,
                Id = postId,
                PostType = PostType.PostText,
                Recipients = new List<Profile>{new Profile {Id = userId}},
                Tags = new List<Profile> { new Profile { Id = userId } },
                Message = "Post Text Message"
            };
        }

        public static Comment CreateComment(string commentId,string forPostId,string userId)
        {
            return new Comment
            {
                Id = commentId,
                Author = new Profile {Id = userId},
                CommentMessage = "Comment",
                CreatedAt = DateTime.Now,
                Recipients = new List<Profile> { new Profile { Id = userId } },
                Tags = new List<Profile> { new Profile { Id = userId } },
                PostType = PostType.Comment,
                ForPostId = forPostId
            };
        }
    }
}
