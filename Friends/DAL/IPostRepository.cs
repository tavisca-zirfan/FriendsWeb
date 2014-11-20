using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Infrastructure.Model;

namespace DAL
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        void DeletePost(Post post);
        void UpdatePost(Post post);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
        void AddLike(string userId, string likeId, string postId, PostType postType, LikeType likeType, DateTime time);
        void RemoveLike(string userId,string postId, PostType postType, LikeType likeType);
        IEnumerable<Post> GetPost(string userId, PostType postType);
    }
}
