using System;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class EntityCreateEvent<T>:EventBase<T>
    {

        public EntityCreateEvent(T entity)
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
