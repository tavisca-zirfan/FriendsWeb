using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using Infrastructure.Container;

namespace Infrastructure.Events
{
    public abstract class EventBase:IEvent
    {
        public IDispatcher Dispatcher { get; set; }

        protected EventBase()
        {
            Dispatcher = ObjectFactory.Resolve<IDispatcher>();
        }
        public abstract void Raise();
    }
}
