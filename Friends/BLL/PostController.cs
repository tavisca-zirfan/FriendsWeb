using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class PostController:IPostController
    {
        public Infrastructure.Model.Post CreatePost(string authorId, string recipientId, string postMessage)
        {
            throw new NotImplementedException();
        }

        public Infrastructure.Model.Comment AddComment(string authorId, string postId, Infrastructure.Model.PostType postType, string comment)
        {
            throw new NotImplementedException();
        }

        public bool RemovePost(string userId, string postId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveComment(string userId, string commentId, Infrastructure.Model.PostType postType)
        {
            throw new NotImplementedException();
        }

        public bool Like(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            throw new NotImplementedException();
        }

        public bool Unlike(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            throw new NotImplementedException();
        }

        public bool Dislike(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            throw new NotImplementedException();
        }

        public bool RevertDislike(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            throw new NotImplementedException();
        }
    }
}
