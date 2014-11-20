using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace BLL
{
    public interface IPostController
    {
        Post CreatePost(string authorId, string recipientId, string postMessage);
        Comment AddComment(string authorId,string postId,PostType postType, string comment);
        bool RemovePost(string userId, string postId);
        bool RemoveComment(string userId, string commentId, PostType postType);
        bool Like(string userId, PostType postType, string destinationId);
        bool Unlike(string userId, PostType postType, string destinationId);
        bool Dislike(string userId, PostType postType, string destinationId);
        bool RevertDislike(string userId, PostType postType, string destinationId);
    }
}
