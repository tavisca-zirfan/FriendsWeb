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
        private List<IEvent> _eventsToSave;
        private List<IEvent> _eventsToLoad;
        public EntityBase()
        {
            _eventsToSave = new List<IEvent>();
            _eventsToLoad = new List<IEvent>();
        }

        public void AddSaveEvent(IEvent raisedEvent)  
        {
            _eventsToSave.Add(raisedEvent);
        }

        public void AddLoadEvent(IEvent raisedEvent)
        {
            _eventsToLoad.Add(raisedEvent);
        }

        public virtual void Save()
        {
            var unitOfWork = ObjectFactory.Resolve<IUnitOfWork>();
            _eventsToSave.ForEach(e=>e.Raise());
            unitOfWork.Commit();
            _eventsToSave.RemoveAll(e => e != null);
        }

        public virtual void Load()
        {
            _eventsToLoad.ForEach(e => e.Raise());
        }
    }
}
