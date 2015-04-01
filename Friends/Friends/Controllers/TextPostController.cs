using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessDomain.DomainObjects;
using Infrastructure.Model;
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
        [HttpGet]
        public PagedList<TextPostDTO> Get(SearchFilter request)
        {
            return new PagedList<TextPostDTO> {Items = PostService.Get(request, UserData).Cast<TextPostDTO>().ToList()};
        }
        [HttpGet]
        public TextPostDTO Get(string id)
        {
            return PostService.Get(id, UserData, PostType.PostText) as TextPostDTO;
        }
        [HttpPut]
        public TextPostDTO Put(TextPostDTO post)
        {
            return PostService.Put(post, UserData) as TextPostDTO;
        }
        [HttpPost]
        public TextPostDTO Post(TextPostDTO post)
        {
            return PostService.Post(post, UserData) as TextPostDTO;
        }
        [HttpDelete]
        public void Delete(TextPostDTO post)
        {
            PostService.Delete(post.Id,UserData);
        }

    }
}
