using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PostRepository:IPostRepository
    {
        public void AddPost(Infrastructure.Model.Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Infrastructure.Model.Post post)
        {
            throw new NotImplementedException();
        }

        public void UpdatePost(Infrastructure.Model.Post post)
        {
            throw new NotImplementedException();
        }

        public void AddComment(Infrastructure.Model.Comment comment)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Infrastructure.Model.Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(Infrastructure.Model.Comment comment)
        {
            throw new NotImplementedException();
        }

        public void AddLike(string userId, string likeId, string postId, Infrastructure.Model.PostType postType, Infrastructure.Model.LikeType likeType, DateTime time)
        {
            throw new NotImplementedException();
        }

        public void RemoveLike(string userId, string postId, Infrastructure.Model.PostType postType, Infrastructure.Model.LikeType likeType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Infrastructure.Model.Post> GetPost(string userId, Infrastructure.Model.PostType postType)
        {
            throw new NotImplementedException();
        }
    }
}
