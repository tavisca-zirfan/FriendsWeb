using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class AddLikeEvent:IEvent
    {
        public string PostId { get; private set; }
        public PostType PostType { get; private set; }
        public string UserId { get; private set; }
        public LikeType LikeType { get; private set; }
        public IDispatcher Dispatcher { get; set; } 
        public AddLikeEvent(string postId,string userId,PostType postType,LikeType likeType)
        {
            PostId = postId;
            UserId = userId;
            PostType = postType;
            LikeType = likeType;
            Dispatcher = new Dispatcher();
        }
        public void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
