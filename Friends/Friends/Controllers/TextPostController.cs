using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BusinessDomain.DomainObjects;
using ServiceLayer;
using ServiceLayer.Model;

namespace Friends.Controllers
{
    public class TextPostController : BaseApiController
    {
        //
        // GET: /TextPost/
        public IPostService PostService { get; set; }

        public TextPostController()
        {
            PostService=new PostService();
        }
        public IEnumerable<TextPostDTO> Get(PostFetchRequest request)
        {
            return PostService.Get(request, UserData).Cast<TextPostDTO>();
        }

        public TextPostDTO Get(string id)
        {
            return PostService.Get(id, UserData, PostType.PostText) as TextPostDTO;
        }

        public TextPostDTO Put(TextPostDTO post)
        {
            return PostService.Put(post, UserData) as TextPostDTO;
        }

        public void Delete(TextPostDTO post)
        {
            PostService.Delete(post.Id,UserData);
        }

    }
}
