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
        string DeletePost(Post post);
        void UpdatePost(Post post);
        Post GetPost(string postId, PostType postType,string userId);
        IEnumerable<Post> GetPosts(PostType postType,string userId);
    }
}
