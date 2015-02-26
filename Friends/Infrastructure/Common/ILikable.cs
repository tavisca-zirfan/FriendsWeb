

namespace Infrastructure.Common
{
    public interface ILikable:ISavable
    {
        void Like(string userId);
        void Dislike(string userId);
    }
}
