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
            
        }

        public static Model.Comment ToBusinessModel(this Comment dbComment,Model.Comment comment =null, int likes = 0, int dislikes = 0)
        {
            if (dbComment == null)
                return null;
            if (comment == null)
                comment = new Model.Comment();
            comment.CommentMessage = dbComment.CommentMessage;
            comment.ForPostId = dbComment.ForPostId;
            return comment;
        }

        public static IEnumerable<Model.Comment> ToBusinessModel(this IEnumerable<Comment> dbComments,
            Dictionary<string, int> likes, Dictionary<string, int> dislikes)
        {
            //return dbComments.Select(dbComment => dbComment.ToBusinessModel(likes[dbComment.CommentId], dislikes[dbComment.CommentId]));
            throw new NotImplementedException();
        }
    }
}
