using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class RemoveLikeEvent : IEvent
    {
        public string Id { get; private set; }
        public PostType PostType { get; private set; }
        public string UserId { get; private set; }
        public IDispatcher Dispatcher { get; set; }
        public RemoveLikeEvent(string id, string userId, PostType postType)
        {
            Id = id;
            UserId = userId;
            PostType = postType;
            Dispatcher = new Dispatcher();
        }
        public void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
