
namespace Infrastructure.Events
{
    public interface IEvent
    {
        void Raise();
    }

    public abstract class EventBase<T> : IEvent
    {
        protected EventBase()
        {
            Dispatcher = new Dispatcher();
        }
        public T Entity { get; set; }
        public IDispatcher Dispatcher { get; set; }
        public abstract void Raise();
    }
}
