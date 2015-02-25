using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.UserEvents
{
    public class ChangePasswordEvent:EventBase<User>
    {
        public ChangePasswordEvent(User user)
        {
            Entity = user;
        }
        public override void Raise(IEvent<User> raisedEvent)
        {
            Dispatcher.Dispatch(this);
        }
    }
}
