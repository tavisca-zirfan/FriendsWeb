
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using BLL;
using Friends.Classes;
using Infrastructure.Model;

namespace Friends.Controllers
{
    public class PostController : ApiController
    {
        public IPostController BPostController { get; set; }
        public CustomPrincipal AuthUser { get; set; }
        public PostController()
        {
            BPostController = new BLL.PostController();
            AuthUser = HttpContext.Current.User as CustomPrincipal;

        }
        [HttpPut]
        public Post CreatePost(string message,string recipient)
        {
            return BPostController.CreatePost(AuthUser.UserId, recipient, message);
        }
        [HttpDelete]
        public bool DeletePost(string postId)
        {
            return BPostController.RemovePost(AuthUser.UserId, postId);
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
