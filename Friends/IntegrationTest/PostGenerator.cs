using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using ServiceLayer.Model;

namespace IntegrationTest
{
    public static class PostGenerator
    {
        public static PostDTO CreateTextPost( string userId)
        {
            return new TextPostDTO
            {
                Author = new ProfileThumbnailDTO { Id = userId },
                CreatedAt = DateTime.Now,
                PostType = PostType.PostText.ToString(),
                Recipients = new List<ProfileThumbnailDTO> { new ProfileThumbnailDTO { Id = userId } },
                Tags = new List<ProfileThumbnailDTO> { new ProfileThumbnailDTO { Id = userId } },
                Message = "Post Text Message"
            };
        }

        public static CommentDTO CreateComment(string forPostId, string userId)
        {
            return new CommentDTO
            {
                Author = new ProfileThumbnailDTO { Id = userId },
                CommentMessage = "Comment",
                CreatedAt = DateTime.Now,
                Recipients = new List<ProfileThumbnailDTO> { new ProfileThumbnailDTO { Id = userId } },
                Tags = new List<ProfileThumbnailDTO> { new ProfileThumbnailDTO { Id = userId } },
                PostType = PostType.Comment.ToString(),
                ForPostId = forPostId
            };
        }
    }
}
