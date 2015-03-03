using System;
using System.Collections.Generic;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.PostResponseEvent;
using Infrastructure.Common;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class Post:EntityBase<string>,ILikable
    {
        public string PostMessage { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
        public List<User> Recipients { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IList<Comment> Comments { get; set; }  
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public PostType PostType;

        public void Like(string userId)
        {
            AddSaveEvent(new RemoveLikeEvent(this.Id,userId,this.PostType));
            AddSaveEvent(new AddLikeEvent(this.Id,userId,this.PostType,LikeType.Like));
            Save();
        }

        public void Dislike(string userId)
        {
            AddSaveEvent(new RemoveLikeEvent(Id,userId,PostType));
            AddSaveEvent(new AddLikeEvent(this.Id, userId, this.PostType, LikeType.Dislike));
            Save();
        }

        public void RemoveComment(string userId,Comment comment)
        {
            var canDelete = (userId == comment.CommentedBy.Id) || (userId == this.Author.Id) ||
                            (userId == this.Recipient.Id);
            if(canDelete)
                AddSaveEvent(new RemoveCommentEvent(null,comment.Id));
        }

        public Comment AddComment(string userId,string commentMessage)
        {
            var comment = new Comment
            {
                CommentedAt = DateTime.Now,
                CommentedBy = new User {Id = userId},
                CommentMessage = commentMessage,
                Id = IdGenerator.GenerateId(),
                PostType = PostType.Comment,
                PostId = this
            };
            AddSaveEvent(new EntityCreateEvent<Comment>(comment));
            this.Save();
            Comments.Add(comment);
            return comment;
        }
    }
}
