using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.Common
{
    public class EntityDeleteEvent<T>:EventBase<T>
    {
        public EntityDeleteEvent(T entity)
        {
            this.Entity = entity;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
