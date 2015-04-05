using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Model;

namespace DomainService
{
    public interface IPostController
    {
        Post CreatePost(Post post,User authUser);
        Post UpdatePost(Post post, User authUser);
        bool RemovePost(string postId, User authUser);
        IEnumerable<Post> GetPosts(SearchFilter filter, IEnumerable<PostType> types, User authUser);
        Post GetPost(string postId, User authUser, string postType);
    }
}
