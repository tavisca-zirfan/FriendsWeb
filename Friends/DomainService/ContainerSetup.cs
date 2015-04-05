using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.PostEvent;
using BusinessDomain.DomainEvents.UserEvents;
using BusinessDomain.DomainObjects;
using DomainService.EventHandlers;
using Infrastructure.Configuration;
using Infrastructure.Events;

namespace DomainService
{
    public class ContainerSetup:IContainerSetup
    {
        public void Setup(Infrastructure.Container.IDependencyContainer container)
        {
            container.Register<IEventHandler<EntityCreateEvent<User>>, UserEventHandler>("usercreate")
                .Register<IEventHandler<EntityUpdateEvent<User>>, UserEventHandler>("userupdate")
                .Register<IEventHandler<EntityDeleteEvent<User>>, UserEventHandler>("userdelete")
                .Register<IEventHandler<LoadFriendsEvent>, UserEventHandler>("loadfriends")
                .Register<IEventHandler<ChangePasswordEvent >, UserEventHandler>("changepassword")
                .Register<IEventHandler<EntityCreateEvent<Post>>, PostHandler>("postcreate")
                .Register<IEventHandler<EntityUpdateEvent<Post>>, PostHandler>("postupdate")
                .Register<IEventHandler<EntityDeleteEvent<Post>>, PostHandler>("postdelete")
                .Register<IEventHandler<AddPostTag>, PostHandler>("addposttag")
                .Register<IEventHandler<AddPostRecipient>, PostHandler>("addpostrecipient")
                .Register<IEventHandler<AddLikeEvent>, PostHandler>("addlikeevent")
                .Register<IEventHandler<RemovePostTag>, PostHandler>("removeposttag")
                .Register<IEventHandler<RemoveLikeEvent>, PostHandler>("removelikeevent")
                .Register<IEventHandler<RemovePostRecipient>, PostHandler>("removepostrecipient")
                .Register<IEventHandler<EntityCreateEvent<Comment>>, PostHandler>("commentcreate")
                .Register<IEventHandler<EntityDeleteEvent<Comment>>, PostHandler>("commentdelete")
                .Register<IUserController,UserController>()
                .Register<IPostController,PostController>()
                ;
        }
    }
}
