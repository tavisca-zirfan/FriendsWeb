
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using DomainService;
using Friends.Classes;
using BusinessDomain.DomainObjects;
using ServiceLayer;
using ServiceLayer.Model;

namespace Friends.Controllers
{
    public class PostController : BaseApiController
    {
        public IPostService PostService { get; set; }
        public PostController()
        {
            PostService = new PostService();

        }
        
        [HttpDelete]
        public void Delete(string postId)
        {
            PostService.Delete(postId,UserData);
        }
        [HttpGet]
        public IEnumerable<PostDTO> Get(PostFetchRequest request)
        {
            return PostService.Get(request,UserData);
        }
        
    }

    public class Filter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
