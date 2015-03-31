using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
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
        public PagedList<TextPostDTO> Get(SearchFilter request)
        {
            return new PagedList<TextPostDTO> {Items = PostService.Get(request, UserData).Cast<TextPostDTO>().ToList()};
        }

        public TextPostDTO Get(string id)
        {
            return PostService.Get(id, UserData, PostType.PostText) as TextPostDTO;
        }

        public TextPostDTO Put(TextPostDTO post)
        {
            return PostService.Put(post, UserData) as TextPostDTO;
        }

        public TextPostDTO Post(TextPostDTO post)
        {
            return PostService.Post(post, UserData) as TextPostDTO;
        }

        public void Delete(TextPostDTO post)
        {
            PostService.Delete(post.Id,UserData);
        }

    }
}
