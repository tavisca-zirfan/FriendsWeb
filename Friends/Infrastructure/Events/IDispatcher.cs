using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Events
{
    public interface IDispatcher
    {
        void Dispatch<T>(IEvent<T> raisedEvent);
    }

    public class Dispatcher : IDispatcher
    {
        public void Dispatch<T>(IEvent<T> raisedEvent)
        {
            throw new NotImplementedException();
        }
    }

}
