using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.UserEvents
{
    public class LoadFriendsEvent:EventBase
    {
        public string UserId { get; private set; }
        public IList<Profile> Friends { get; private set; }
        public LoadFriendsEvent(string userId,IList<Profile> friends)
        {
            UserId = userId;
            Friends = friends;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
