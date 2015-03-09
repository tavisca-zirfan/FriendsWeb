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
        Post CreatePost(string authorId, string recipientId, string postMessage);
        bool RemovePost(string userId, string postId);
        IEnumerable<Post> GetPosts(SearchFilter filter);
    }
}
