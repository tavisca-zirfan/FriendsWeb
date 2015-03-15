using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class RemoveLikeEvent : EventBase
    {
        public string PostId { get; private set; }
        public string UserId { get; private set; }
        public RemoveLikeEvent(string postId, string userId)
        {
            PostId = postId;
            UserId = userId;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
