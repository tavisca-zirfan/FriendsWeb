
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainObjects;
using DAL;
using Infrastructure.Container;
using Infrastructure.Data;
using Infrastructure.Events;

namespace DomainService.EventHandlers
{
    public class UserEventHandler : IEventHandler<EntityCreateEvent<User>>,IEventHandler<EntityUpdateEvent<User>>,IEventHandler<EntityDeleteEvent<User>>
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public UserEventHandler()
        {
            _unitOfWork = ObjectFactory.Resolve<IUnitOfWork>();
            _userRepository = ObjectFactory.Resolve<IUserRepository>();
        }
        public void Handle(EntityCreateEvent<User> eventObject)
        {

            _userRepository.AddUser(eventObject.Entity);
        }

        public void Handle(EntityUpdateEvent<User> eventObject)
        {
            _userRepository.UpdateCredential(eventObject.Entity);
        }

        public void Handle(EntityDeleteEvent<User> eventObject)
        {
            _userRepository.DeleteCredential(eventObject.Entity.Id);
        }
    }

}
