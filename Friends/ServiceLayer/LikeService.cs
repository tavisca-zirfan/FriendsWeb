using BusinessDomain.DomainObjects;
using DomainService;
using Infrastructure.Container;
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

        public LikeService()
        {
            PostController = ObjectFactory.Resolve<IPostController>();
        }
        public void Post(string postId, PostType postType, LikeType likeType, UserDTO authUser)
        {
            var user = authUser.ToBusinessModel();
            var post = PostController.GetPost(postId, user, postType);
            if (likeType == LikeType.Like)
            {
                post.Like(user);
            }
            else
            {
                post.Dislike(user);
            }
            post.Save();
        }
    }

}
