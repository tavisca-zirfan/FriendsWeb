

using BusinessDomain.DomainObjects;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface ILikeService
    {
        void Post(string postId, PostType postType, LikeType likeType, UserDTO authUser);
    }
}
