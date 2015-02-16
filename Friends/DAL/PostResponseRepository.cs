using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using Model = BusinessDomain.DomainObjects;
using Infrastructure.Data;

namespace DAL
{
    public class PostResponseRepository:IPostResponseRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }
        private FriendsContext Db;
        public PostResponseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            if (unitOfWork == null)
                Db = new FriendsContext();
            else
            {
                Db = UnitOfWork.GetTransactionObject() as FriendsContext;
            }
        }

        public PostResponseRepository():this(null){}

        public void AddComment(string postId,Model.Comment comment)
        {
            var dbComment = new Comment();
            comment.ToDbModel(dbComment,postId);
            Db.Comments.Add(dbComment);
        }

        public void UpdateComment(Model.Comment comment)
        {
            throw new NotImplementedException();
        }

        public string DeleteComment(Model.Comment comment)
        {
            var dbComment =
                Db.Comments.FirstOrDefault(
                    c => c.CommentId == comment.CommentId && c.UserId == comment.CommentedBy.UserId);
            if (dbComment == null)
                return null;
            RemoveLike(new List<string>{dbComment.CommentId},Model.PostType.Comment );
            Db.Comments.Remove(dbComment);
            return dbComment.CommentId;
        }

        public IEnumerable<string> DeleteComment(string postId, Model.PostType postType)
        {
            var dbComment = Db.Comments.Where(c => c.TypeId == postId && c.Type == postType.ToString());
            var deletedIds = dbComment.Select(c => c.CommentId).ToList();
            Db.Comments.RemoveRange(dbComment);
            RemoveLike(deletedIds,Model.PostType.Comment);
            return deletedIds;
        }

        public void AddLike(string userId, string likeId, string postId, Model.PostType postType, Model.LikeType likeType, DateTime time)
        {
            Db.Likes.Add(new Like {LikeId = likeId,LikeType = (int)likeType,Time = time,Type=postType.ToString(),TypeId=postId,UserId = userId});
        }

        public void RemoveLike(string userId, string postId, Model.PostType postType, Model.LikeType likeType)
        {
            Db.Likes.RemoveRange(Db.Likes.Where(
                l =>
                    l.TypeId == postId && l.UserId == userId && l.Type == postType.ToString() &&
                    l.LikeType == (int) likeType));
        }

        public void RemoveLike(List<string> postIds, Model.PostType postType)
        {
            Db.Likes.RemoveRange(Db.Likes.Where(l =>postIds.Contains(l.TypeId)  && l.Type == postType.ToString()));
        }


        public int GetLike(string postId, Model.PostType postType,Model.LikeType likeType, string userId)
        {
            return
                Db.Likes.Count(
                    l =>
                        l.LikeType == (int) likeType && l.TypeId == postId && l.Type == postType.ToString() &&
                        (l.UserId == userId || userId == ""));
        }
    }
}
