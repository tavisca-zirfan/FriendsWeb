
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DomainService;
using Friends.Classes;
using BusinessDomain.DomainObjects;
using Infrastructure.Model;
using ServiceLayer;
using ServiceLayer.Model;

namespace Friends.Controllers
{
    public class PostController : BaseApiController
    {
        public IPostService PostService { get; set; }
        public ILikeService LikeService { get; set; }
        public PostController()
        {
            PostService = new PostService();
            LikeService = (ILikeService) PostService;
        }
        
        [HttpDelete]
        public void Delete(string postId)
        {
            PostService.Delete(postId,UserData);
        }
        [HttpGet]
        public PagedList<PostDTO> Get([FromUri]SearchFilter request)
        {
            return new PagedList<PostDTO> {Items = PostService.Get(request, UserData).ToList()};
        }

        
        [HttpPost]
        public PostDTO Like(PostDTO post)
        {
            PostType postType;
            if (Enum.TryParse(post.PostType, out postType))
                return LikeService.Post(post.Id, postType, LikeType.Like, UserData);
            throw new ArgumentException("Invalid PostType");

        }
        [HttpPost]
        public PostDTO Dislike(PostDTO post)
        {
            PostType postType;
            if (Enum.TryParse(post.PostType, out postType))
                return LikeService.Post(post.Id, postType, LikeType.Dislike, UserData);
            throw new ArgumentException("Invalid PostType");

        }

    }

    
}
