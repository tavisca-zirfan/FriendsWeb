using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Infrastructure.Model;
using BusinessDomain.DomainObjects;

namespace DAL
{
    public interface IPostRepository
    {
        void AddPost(Post post);
        string DeletePost(Post post);
        void UpdatePost(Post post);
        Post GetPost(string postId, PostType postType);
        IEnumerable<Post> GetPosts(SearchFilter filter,IEnumerable<PostType> types);
        void AddLike(string postId, string userId, LikeType likeType);
        void RemoveLike(string postId, string userId);
        void AddRecipient(string postId, string userId);
        void RemoveRecipient(string postId, string userId);
        void AddTag(string postId, string userId);
        void RemoveTag(string postId, string userId);
    }
}
