using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class AddLikeEvent:EventBase
    {
        public string PostId { get; private set; }
        public PostType PostType { get; private set; }
        public string UserId { get; private set; }
        public LikeType LikeType { get; private set; }
        public AddLikeEvent(string postId,string userId,LikeType likeType)
        {
            PostId = postId;
            UserId = userId;
            LikeType = likeType;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
