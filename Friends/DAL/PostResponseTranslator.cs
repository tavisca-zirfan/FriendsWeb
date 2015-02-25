using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model = BusinessDomain.DomainObjects;
using FriendsDb.Models;

namespace DAL
{
    public static class PostResponseTranslator
    {
        public static void ToDbModel(this Model.Comment comment, Comment dbComment,string postId="")
        {
            dbComment.CommentId = comment.Id;
            dbComment.CommentMessage = comment.CommentMessage;
            if (comment.CommentedAt.HasValue)
                dbComment.CommentTime = comment.CommentedAt.Value;
            if (comment.CommentedBy != null)
                dbComment.UserId = comment.CommentedBy.Id;
            if (comment.PostType.HasValue)
                dbComment.Type = comment.PostType.Value.ToString();
            if (!string.IsNullOrEmpty(postId))
                dbComment.TypeId = postId;
        }

        public static Model.Comment ToBusinessModel(this Comment dbComment, int likes = 0, int dislikes = 0)
        {
            if (dbComment == null)
                return null;
            var comment = new Model.Comment
            {
                CommentedBy = new Model.User
                {
                    Id = dbComment.UserId
                },
                CommentedAt = dbComment.CommentTime,
                Likes = likes,
                Dislikes = dislikes,
                Id = dbComment.CommentId,
                CommentMessage = dbComment.CommentMessage,
                PostType = Model.PostType.Comment
            };
            
            return comment;
        }

        public static IEnumerable<Model.Comment> ToBusinessModel(this IEnumerable<Comment> dbComments,
            Dictionary<string, int> likes, Dictionary<string, int> dislikes)
        {
            return dbComments.Select(dbComment => dbComment.ToBusinessModel(likes[dbComment.CommentId], dislikes[dbComment.CommentId]));
        }
    }
}
