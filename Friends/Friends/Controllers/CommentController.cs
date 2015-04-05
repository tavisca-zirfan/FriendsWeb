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
    public class CommentController : BaseApiController
    {
        //
        // GET: /Comment/

        public IPostService PostService { get; set; }

        public CommentController()
        {
            PostService=new PostService();
        }
        [HttpGet]
        public IEnumerable<CommentDTO> Get(SearchFilter request)
        {
            return PostService.Get(request, UserData).Cast<CommentDTO>();
        }
        [HttpGet]
        public CommentDTO Get(string id)
        {
            return PostService.Get(id, UserData, PostType.Comment.ToString()) as CommentDTO;
        }
        [HttpPut]
        public CommentDTO Put(CommentDTO post)
        {
            return PostService.Put(post, UserData) as CommentDTO;
        }
        [HttpPost]
        public CommentDTO Post(CommentDTO post)
        {
            return PostService.Post(post, UserData) as CommentDTO;
        }
        [HttpDelete]
        public void Delete(CommentDTO post)
        {
            PostService.Delete(post.Id,UserData);
        }

    }
}
