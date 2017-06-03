using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Infrastructure.Data;
using BusinessDomain.DomainObjects;

namespace DomainService
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
        public BusinessDomain.DomainObjects.Comment AddComment(string authorId, string postId, BusinessDomain.DomainObjects.PostType postType, string commentMessage)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Author = new Profile { Id = authorId },
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

        public bool RemoveComment(string userId, string commentId, BusinessDomain.DomainObjects.PostType postType)
        {
            try
            {
                PostResponseRepository.DeleteComment(new Comment { Author = new Profile { Id = userId }, Id = commentId, PostType = postType });
                PostResponseRepository.RemoveLike(new List<string>{commentId},PostType.Comment );
                UnitOfWork.Commit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Like(string userId, BusinessDomain.DomainObjects.PostType postType, string destinationId)
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

        public bool Unlike(string userId, BusinessDomain.DomainObjects.PostType postType, string destinationId)
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

        public bool Dislike(string userId, BusinessDomain.DomainObjects.PostType postType, string destinationId)
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

        public bool RevertDislike(string userId, BusinessDomain.DomainObjects.PostType postType, string destinationId)
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
