using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class EntityDeleteEvent<T>:EventBase<T>
    {
        public IDispatcher Dispatcher { get; set; }

        public EntityDeleteEvent(T entity)
        {
            this.Entity = entity;
        }
        public override void Raise(IEvent<T> raisedEvent)
        {
            Dispatcher.Dispatch(this);
        }
    }
}
