using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessDomain.DomainObjects;
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
        public IEnumerable<CommentDTO> Get(PostFetchRequest request)
        {
            return PostService.Get(request, UserData).Cast<CommentDTO>();
        }

        public CommentDTO Get(string id)
        {
            return PostService.Get(id, UserData, PostType.PostText) as CommentDTO;
        }

        public CommentDTO Put(CommentDTO post)
        {
            
            return PostService.Put(post, UserData) as CommentDTO;
        }

        public void Delete(CommentDTO post)
        {
            PostService.Delete(post.Id,UserData);
        }

    }
}
