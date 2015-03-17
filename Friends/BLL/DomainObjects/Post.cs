using System;
using System.Collections.Generic;
using System.Linq;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.PostEvent;
using Infrastructure.Common;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public abstract class Post:EntityBase<string>,ILikable
    {
        public Profile Author { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Profile> Likes; 
        public IList<Profile> Recipients { get; set; }
        public IList<Profile> Tags { get; set; }
        public IList<Profile> Dislikes { get; set; }

        public int NumberOfLikes
        {
            get { return Likes.Count; }
        }

        public int NumberOfDislikes
        {
            get { return Dislikes.Count; }
        }

        public PostType PostType;

        public void Like(string userId)
        {
            AddSaveEvent(new RemoveLikeEvent(this.Id,userId));
            AddSaveEvent(new AddLikeEvent(this.Id,userId,LikeType.Like));
        }

        public void Dislike(string userId)
        {
            AddSaveEvent(new RemoveLikeEvent(Id,userId));
            AddSaveEvent(new AddLikeEvent(this.Id, userId, LikeType.Dislike));
        }

        public void RemoveComment(string userId,Comment comment)
        {
            var canDelete = (userId == comment.Author.Id) || (userId == this.Author.Id) ||
                            (this.Recipients.Select(p=>p.Id).Contains(userId));
            if(canDelete)
                AddSaveEvent(new EntityDeleteEvent<Post>(comment));
        }

        public Comment AddComment(string userId,string commentMessage)
        {
            var comment = new Comment
            {
                CreatedAt = DateTime.Now,
                Author = new Profile {Id = userId},
                CommentMessage = commentMessage,
                Id = IdGenerator.GenerateId(),
                PostType = PostType.Comment,
                ForPostId = this.Id
            };
            AddSaveEvent(new EntityCreateEvent<Post>(comment));
            Comments.Add(comment);
            return comment;
        }

        public void AddRecipients(IEnumerable<string> userIds)
        {
            AddSaveEvent(new AddPostRecipient(this,userIds));
        }

        public void AddTags(IEnumerable<string> userIds)
        {
            AddSaveEvent(new AddPostTag(this,userIds));
        }

        public void RemoveRecipients(IEnumerable<string> userIds)
        {
            AddSaveEvent(new RemovePostRecipient(this, userIds));
        }

        public void RemoveTags(IEnumerable<string> userIds)
        {
            AddSaveEvent(new RemovePostTag(this, userIds));
        }
    }
}
