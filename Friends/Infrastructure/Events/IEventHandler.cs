

using Infrastructure.Data;

namespace Infrastructure.Events
{
    public interface IEventHandler<T>
    {
        void Handle(T eventObject);
    }
}
