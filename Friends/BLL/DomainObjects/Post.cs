﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.PostEvent;
using BusinessDomain.Interface;
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
        private IList<Profile> _recipients;

        public IList<Profile> Recipients
        {
            get { return _recipients; }
            set
            {
                if (_recipients == null)
                    _recipients = value;
                else
                {
                    var idsToBeRemoved = _recipients.Select(r => r.Id).Except(value.Select(v => v.Id)).Distinct();
                    var idsToBeAdded = value.Select(v => v.Id).Except(_recipients.Select(r => r.Id)).Distinct();
                    AddSaveEvent(new RemovePostRecipient(this, idsToBeRemoved));
                    AddSaveEvent(new AddPostRecipient(this, idsToBeAdded));
                }
            }
            
        }
        private IList<Profile> _tags;

        public IList<Profile> Tags
        {
            get { return _tags; }
            set
            {
                if (_tags == null)
                    _tags = value;
                else
                {
                    var idsToBeRemoved = _tags.Select(t => t.Id).Except(value.Select(v => v.Id)).Distinct();
                    var idsToBeAdded = value.Select(v => v.Id).Except(_tags.Select(t => t.Id)).Distinct();
                    AddSaveEvent(new RemovePostTag(this,idsToBeRemoved));
                    AddSaveEvent(new AddPostTag(this,idsToBeAdded));
                }
            }
        }

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

        public void Like(User user)
        {
            AddSaveEvent(new RemoveLikeEvent(this.Id,user.Id));
            AddSaveEvent(new AddLikeEvent(this.Id,user.Id,LikeType.Like));
        }

        public void Dislike(User user)
        {
            AddSaveEvent(new RemoveLikeEvent(Id,user.Id));
            AddSaveEvent(new AddLikeEvent(this.Id, user.Id, LikeType.Dislike));
        }

        public void RemoveComment(User user,Comment comment)
        {
            var canDelete = (user.Id == comment.Author.Id) || (user.Id == this.Author.Id) ||
                            (this.Recipients.Select(p=>p.Id).Contains(user.Id));
            if(canDelete)
                AddSaveEvent(new EntityDeleteEvent<Post>(comment));
        }

        public Comment AddComment(User user,string commentMessage)
        {
            var comment = new Comment
            {
                CreatedAt = DateTime.Now,
                Author = new Profile {Id = user.Id},
                CommentMessage = commentMessage,
                Id = IdGenerator.GenerateId(),
                PostType = PostType.Comment,
                ForPostId = this.Id
            };
            AddSaveEvent(new EntityCreateEvent<Post>(comment));
            Comments.Add(comment);
            return comment;
        }

        public void AddRecipients(IEnumerable<string> userIds,User user)
        {
            var enumerable = userIds as IList<string> ?? userIds.ToList();
            foreach (var userId in enumerable.ToList())
            {
                Tags.Add(new Profile { Id = userId });
            }
            AddSaveEvent(new AddPostRecipient(this,enumerable));
        }

        public void AddTags(IEnumerable<string> userIds,User user)
        {
            var enumerable = userIds as IList<string> ?? userIds.ToList();
            foreach (var userId in enumerable.ToList())
            {
                Tags.Add(new Profile{Id=userId});
            }
            AddSaveEvent(new AddPostTag(this,enumerable));
        }

        public void RemoveRecipients(IEnumerable<string> userIds,User user)
        {
            var enumerable = userIds as IList<string> ?? userIds.ToList();
            foreach (var userId in enumerable)
            {
                Tags.Remove(Tags.FirstOrDefault(t => t.Id == userId));
            }
            AddSaveEvent(new RemovePostRecipient(this, enumerable));
        }

        public void RemoveTags(IEnumerable<string> userIds,User user)
        {
            var enumerable = userIds as IList<string> ?? userIds.ToList();
            foreach (var userId in enumerable)
            {
                Recipients.Remove(Recipients.FirstOrDefault(t => t.Id == userId));
            }
            AddSaveEvent(new RemovePostTag(this, enumerable));
        }
    }



}
