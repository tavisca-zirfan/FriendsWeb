using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainService;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IPostService
    {
        IEnumerable<PostDTO> Get(PostFetchRequest request);
        PostDTO Get(string postId);
        void Post(PostDTO request);
        void Delete(string postId);
    }

    public class PostService : IPostService
    {
        public IPostController PostController { get; set; }
        public IEnumerable<PostDTO> Get(PostFetchRequest request)
        {
            throw new NotImplementedException();
        }

        public PostDTO Get(string postId)
        {
            throw new NotImplementedException();
        }

        public void Post(PostDTO request)
        {
            throw new NotImplementedException();
        }

        public void Delete(string postId)
        {
            throw new NotImplementedException();
        }
    }

}
