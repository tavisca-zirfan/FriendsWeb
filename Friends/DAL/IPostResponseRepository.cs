using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;
using BusinessDomain.DomainObjects;

namespace DAL
{
    public interface IPostResponseRepository
    {
        void AddComment(string postId,Comment comment);
        void UpdateComment(Comment comment);
        string DeleteComment(Comment comment);
        IEnumerable<string> DeleteComment(string postId, PostType postType);
        int GetLike(string postId, PostType postType,LikeType likeType, string userId);
        void AddLike(string userId, string likeId, string postId, PostType postType, LikeType likeType, DateTime time);
        void RemoveLike(string userId, string postId, PostType postType, LikeType likeType);
        void RemoveLike(List<string> postIds, PostType postType);
    }
}
