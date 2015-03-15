
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using DomainService;
using Friends.Classes;
using BusinessDomain.DomainObjects;
using ServiceLayer;

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
        public void DeletePost(string postId)
        {
            PostService.Delete(postId,UserData);
        }
        //[HttpGet]
        //public IEnumerable<Post> GetPosts()
        //{
        //    return BPostController.GetPosts("");
        //}

        [HttpGet]
        public string GetFilterObject([FromUri] List<Filter> filter)
        {
            var str = "";
            filter.ForEach(f => str = str + f.Name + ":" + f.Value + ";");
            return str;
        }
    }

    public class Filter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
