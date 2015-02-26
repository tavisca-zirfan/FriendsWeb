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
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }

    

}
