using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IPostService
    {
        IEnumerable<PostDTO> Get(PostFetchRequest request);
        IEnumerable<PostDTO> Get(string postId);
        void Post(PostDTO request);
        void Delete(string postId);
    }
}
