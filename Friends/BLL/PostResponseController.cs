using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Infrastructure.Data;
using Infrastructure.Model;

namespace BLL
{
    public class PostResponseController:IPostResponseController
    {
        public IPostResponseRepository PostResponseRepository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public PostResponseController()
        {
            UnitOfWork = new UnitOfWork();
            PostResponseRepository=new PostResponseRepository(UnitOfWork);

        }
        public Infrastructure.Model.Comment AddComment(string authorId, string postId, Infrastructure.Model.PostType postType, string commentMessage)
        {
            var comment = new Comment
            {
                CommentId = Guid.NewGuid().ToString(),
                CommentedAt = DateTime.UtcNow,
                CommentedBy = new User { UserId = authorId },
                CommentMessage = commentMessage,
                PostType = postType
            };
            try
            {
                PostResponseRepository.AddComment(postId,comment);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return null;
            }
            return comment;
        }

        public bool RemoveComment(string userId, string commentId, Infrastructure.Model.PostType postType)
        {
            try
            {
                PostResponseRepository.DeleteComment(new Comment { CommentedBy = new User { UserId = userId }, CommentId = commentId, PostType = postType });
                PostResponseRepository.RemoveLike(new List<string>{commentId},PostType.Comment );
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Like(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            try
            {
                int like = PostResponseRepository.GetLike(destinationId, postType, LikeType.Like, userId);
                if (like > 0)
                    return true;
                PostResponseRepository.RemoveLike(userId,destinationId,postType,LikeType.Dislike);
                PostResponseRepository.AddLike(userId, Guid.NewGuid().ToString(), destinationId, postType, LikeType.Like, DateTime.UtcNow);
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Unlike(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            try
            {
                PostResponseRepository.RemoveLike(userId, destinationId, postType, LikeType.Like);
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Dislike(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            try
            {
                int dislike = PostResponseRepository.GetLike(destinationId, postType, LikeType.Dislike, userId);
                if (dislike > 0)
                    return true;
                PostResponseRepository.RemoveLike(userId, destinationId, postType, LikeType.Like);
                PostResponseRepository.AddLike(userId, Guid.NewGuid().ToString(), destinationId, postType, LikeType.Dislike, DateTime.UtcNow);
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RevertDislike(string userId, Infrastructure.Model.PostType postType, string destinationId)
        {
            try
            {
                PostResponseRepository.RemoveLike(userId, destinationId, postType, LikeType.Dislike);
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
