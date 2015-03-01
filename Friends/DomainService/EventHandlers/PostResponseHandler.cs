using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainObjects;
using DAL;
using Infrastructure.Container;
using Infrastructure.Events;

namespace DomainService.EventHandlers
{
    public class PostResponseHandler:IEventHandler<EntityCreateEvent<Comment>>,IEventHandler<EntityDeleteEvent<Comment>>
    {
        private IPostResponseRepository _postResponseRepository;

        public PostResponseHandler()
        {
            _postResponseRepository = ObjectFactory.Resolve<IPostResponseRepository>();
        }
        public void Handle(EntityDeleteEvent<Comment> eventObject)
        {
            _postResponseRepository.DeleteComment(eventObject.Entity);
        }

        public void Handle(EntityCreateEvent<Comment> eventObject)
        {
            _postResponseRepository.AddComment(eventObject.Entity.PostId,eventObject.Entity);
        }
    }
}
