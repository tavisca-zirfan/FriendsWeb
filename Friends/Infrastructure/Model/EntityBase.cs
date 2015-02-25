using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;

namespace Infrastructure.Model
{
    public class EntityBase<T>
    {
        public T Id { get; set; }
        private List<IEvent> _events;
        public EntityBase()
        {
            _events = new List<IEvent>();
        }

        public void AddEvent(IEvent raisedEvent)  
        {
            _events.Add(raisedEvent);
        }

        public virtual void Save()
        {
            
        }

        public virtual void Load()
        {
            
        }
    }
}
