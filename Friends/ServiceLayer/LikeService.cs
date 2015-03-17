

using BusinessDomain.DomainObjects;
using DomainService;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface ILikeService
    {
        void Post(string postId, PostType postType, LikeType likeType, UserDTO authUser);
    }
    public class LikeService : ILikeService
    {
        public IPostController PostController { get; set; }
        public void Post(string postId, PostType postType, LikeType likeType, UserDTO authUser)
        {
            
        }
    }

}
