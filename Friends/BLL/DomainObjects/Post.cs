using System;
using System.Collections.Generic;
using System.IO;
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
        public DateTime LastUpdate { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<Profile> Likes { get; set; }
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
            get
            {
                return Likes!=null ? Likes.Count : 0;
            }
        }

        public int NumberOfDislikes
        {
            get { return Dislikes!=null? Dislikes.Count:0; }
        }

        public PostType PostType;

        public void Like(User user)
        {
            if (this.Dislikes.Select(p => p.Id).Contains(user.Id))
            {
                AddSaveEvent(new RemoveLikeEvent(this.Id, user.Id));
                var itemToBeRemoved = Dislikes.Single(p => p.Id == user.Id);
                Dislikes.Remove(itemToBeRemoved);
            }
            else if (!this.Likes.Select(p => p.Id).Contains(user.Id))
            {
                AddSaveEvent(new AddLikeEvent(this.Id, user.Id, LikeType.Like));
                Likes.Add(user.ToProfile());
            }
            else
                throw new InvalidDataException("Already it has been liked");
            Update();
        }

        public void Dislike(User user)
        {
            if (this.Likes.Select(p => p.Id).Contains(user.Id))
            {
                AddSaveEvent(new RemoveLikeEvent(this.Id, user.Id));
                var itemToBeRemoved = Likes.Single(p => p.Id == user.Id);
                Likes.Remove(itemToBeRemoved);
            }
            else if (!this.Dislikes.Select(p => p.Id).Contains(user.Id))
            {
                AddSaveEvent(new AddLikeEvent(this.Id, user.Id, LikeType.Dislike));
                Dislikes.Add(user.ToProfile());
            }
            else
                throw new InvalidDataException("Already it has been disliked");
            Update();
        }

        public void RemoveComment(User user,Comment comment)
        {
            var canDelete = (user.Id == comment.Author.Id) || (user.Id == this.Author.Id) ||
                            (this.Recipients.Select(p=>p.Id).Contains(user.Id));
            if(canDelete)
                AddSaveEvent(new EntityDeleteEvent<Post>(comment));
            Update();
        }

        public Comment AddComment(User user,Comment comment)
        {
            try
            {
                comment.Id = IdGenerator.GenerateId();
                comment.CreatedAt = DateTime.UtcNow;
                comment.LastUpdate = DateTime.UtcNow;
                comment.Author = user.ToProfile();
                this.LastUpdate = DateTime.UtcNow;
                AddSaveEvent(new EntityCreateEvent<Post>(comment));
                Update();
                Comments.Add(comment);
                return comment;
            }
            catch (Exception ex)
            {
                return null;
            }
            
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

        public void Update()
        {
            this.LastUpdate = DateTime.UtcNow;
            AddSaveEvent(new EntityUpdateEvent<Post>(this));
            Save();
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
