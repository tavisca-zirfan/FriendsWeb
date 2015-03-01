using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainObjects;
using DAL;
using Infrastructure.Container;
using Infrastructure.Data;
using Infrastructure.Events;

namespace DomainService.EventHandlers
{
    public class PostHandler:IEventHandler<EntityCreateEvent<Post>>,IEventHandler<EntityDeleteEvent<Post>>
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
    }
}
