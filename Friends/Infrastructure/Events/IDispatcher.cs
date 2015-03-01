using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Container;
using Infrastructure.Data;
using Microsoft.Practices.ObjectBuilder2;

namespace Infrastructure.Events
{
    public interface IDispatcher
    {
        void Dispatch<T>(T raisedEvent);
    }

    public class Dispatcher : IDispatcher
    {

        public void Dispatch<T>(T raisedEvent)
        {
            var handlers = ObjectFactory.ResolveAll<IEventHandler<T>>();
            handlers.ForEach(h=>h.Handle(raisedEvent));
        }
    }

}
