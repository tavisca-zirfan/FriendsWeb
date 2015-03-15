using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            post.ToBusinessModel(comment);
            return comment;
        }

        public override void InsertPost(BusinessDomain.DomainObjects.Post post)
        {
            var dbComment = new Comment();
            var comment = post as BusinessDomain.DomainObjects.Comment;
            if(comment==null)
                throw new InvalidCastException("Post is not of type comment");
            comment.ToDbModel(dbComment);
            Db.Comments.Add(dbComment);
        }

        public override void RemovePost(BusinessDomain.DomainObjects.Post post)
        {
            Db.PostRecipients.RemoveRange(Db.PostRecipients.Where(r => post.Id==r.PostId));
            Db.PostTags.RemoveRange(Db.PostTags.Where(t => post.Id==t.PostId));
            Db.Likes.RemoveRange(Db.Likes.Where(l => post.Id==l.PostId));
            Db.Comments.Remove(Db.Comments.First(c => post.Id==c.CommentId));
        }

        public override void RemovePosts(IEnumerable<BusinessDomain.DomainObjects.Post> posts)
        {
            var enumerable = posts.Select(p=>p.Id).ToList();
            if (posts == null || !enumerable.Any())
                return;
            Db.PostRecipients.RemoveRange(Db.PostRecipients.Where(r => enumerable.Contains(r.PostId)));
            Db.PostTags.RemoveRange(Db.PostTags.Where(t => enumerable.Contains(t.PostId)));
            Db.Likes.RemoveRange(Db.Likes.Where(l => enumerable.Contains(l.PostId)));
            Db.Comments.RemoveRange(Db.Comments.Where(c => enumerable.Contains(c.CommentId)));
        }



        public override IQueryable<Post> IncludeTables(IQueryable<Post> posts)
        {
            return posts.Include(Models.PostType.Comment.ToString());
        }
    }
}
