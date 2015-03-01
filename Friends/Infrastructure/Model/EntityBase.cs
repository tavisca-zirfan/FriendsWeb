using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Common;
using Infrastructure.Container;
using Infrastructure.Data;
using Infrastructure.Events;

namespace Infrastructure.Model
{
    public class EntityBase<T>:ISavable
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
            var unitOfWork = ObjectFactory.Resolve<IUnitOfWork>();
            _events.ForEach(e=>e.Raise());
            unitOfWork.Commit();
            unitOfWork.Refresh();
        }

        public virtual void Load()
        {
            
        }
    }
}
