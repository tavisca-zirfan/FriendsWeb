using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.PostEvent;
using BusinessDomain.DomainObjects;
using DAL;
using Infrastructure.Container;
using Infrastructure.Data;
using Infrastructure.Events;

namespace DomainService.EventHandlers
{
    public class PostHandler:IEventHandler<EntityCreateEvent<Post>>,IEventHandler<EntityDeleteEvent<Post>>
        ,IEventHandler<AddPostTag>,IEventHandler<AddPostRecipient>,IEventHandler<AddLikeEvent>,IEventHandler<RemoveLikeEvent>
        ,IEventHandler<RemovePostTag>,IEventHandler<RemovePostRecipient>
    {
        private IPostRepository _postRepository;
        private IUnitOfWork _unitOfWork;

        public PostHandler()
        {
            _postRepository = ObjectFactory.Resolve<IPostRepository>();
            _unitOfWork = ObjectFactory.Resolve<IUnitOfWork>();
        }
        public void Handle(EntityDeleteEvent<Post> eventObject)
        {
            _postRepository.DeletePost(eventObject.Entity);
        }

        public void Handle(EntityCreateEvent<Post> eventObject)
        {
            _postRepository.AddPost(eventObject.Entity);
        }

        public void Handle(AddPostTag eventObject)
        {
            foreach (var userId in eventObject.UserIds)
            {
                _postRepository.AddTag(eventObject.Post.Id,userId);
            }
            
        }

        public void Handle(AddPostRecipient eventObject)
        {
            foreach (var userId in eventObject.UserIds)
            {
                _postRepository.AddRecipient(eventObject.Post.Id, userId);
            }
        }

        public void Handle(AddLikeEvent eventObject)
        {
            _postRepository.AddLike(eventObject.PostId,eventObject.UserId,eventObject.LikeType);
        }

        public void Handle(RemoveLikeEvent eventObject)
        {
            _postRepository.RemoveLike(eventObject.PostId, eventObject.UserId);
        }

        public void Handle(RemovePostRecipient eventObject)
        {
            foreach (var userId in eventObject.UserIds)
            {
                _postRepository.RemoveRecipient(eventObject.Post.Id,userId);
            }
        }

        public void Handle(RemovePostTag eventObject)
        {
            foreach (var userId in eventObject.UserIds)
            {
                _postRepository.RemoveTag(eventObject.Post.Id, userId);
            }
        }
    }
}
