using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Events
{
    public interface IDispatcher
    {
        void Dispatch(IEvent raisedEvent);
    }

    public class Dispatcher : IDispatcher
    {
        public void Dispatch(IEvent raisedEvent)
        {
            throw new NotImplementedException();
        }
    }

}
