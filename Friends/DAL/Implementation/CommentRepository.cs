using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models=BusinessDomain.DomainObjects;
using FriendsDb.Models;

namespace DAL.Implementation
{
    class CommentRepository:BasePostTypeRepository
    {
        public override BusinessDomain.DomainObjects.Post ParsePost(FriendsDb.Models.Post post)
        {
            var comment = post.Comment.ToBusinessModel();
            //post.ToBusinessModel(comment);
            return comment;
        }

        public override void InsertPost(BusinessDomain.DomainObjects.Post post)
        {
            var dbComment = new Comment();
            var comment = post as Models.Comment;
            if(comment==null)
                throw new InvalidCastException("Post is not of type comment");
            comment.ToDbModel(dbComment);
            Db.Comments.Add(dbComment);
        }

        public override void RemovePost(FriendsDb.Models.Post post)
        {
            var comment = Db.Comments.FirstOrDefault(c => c.CommentId == post.Pid);
            Db.Comments.Remove(comment);
        }
    }
}
