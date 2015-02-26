

using BusinessDomain.DomainObjects;

namespace ServiceLayer
{
    public interface ILikeService
    {
        void Post(string postId, PostType postType, LikeType likeType);
    }
}
