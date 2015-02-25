using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class EntityUpdateEvent<T>:EventBase<T>
    {
        public EntityUpdateEvent(T entity)
        {
            Entity = entity;
        }
        public IDispatcher Dispatcher { get; set; }
        public override void Raise(IEvent<T> raisedEvent)
        {
            Dispatcher.Dispatch(this);
        }
    }
}
