using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.UserEvents
{
    public class AddFriendEvent:EventBase
    {
        public string From { get; private set; }
        public string To { get; private set; }

        public AddFriendEvent(string from,string to)
        {
            From = from;
            To = to;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
