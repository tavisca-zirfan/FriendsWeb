using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace DomainService
{
    public interface IPostResponseController
    {
        Comment AddComment(string authorId, string postId, PostType postType, string comment);
        bool RemoveComment(string userId, string commentId, PostType postType);
        bool Like(string userId, PostType postType, string destinationId);
        bool Unlike(string userId, PostType postType, string destinationId);
        bool Dislike(string userId, PostType postType, string destinationId);
        bool RevertDislike(string userId, PostType postType, string destinationId);
    }
}
