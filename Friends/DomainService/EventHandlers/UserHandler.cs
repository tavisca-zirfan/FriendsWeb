
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainObjects;
using DAL;
using Infrastructure.Data;
using Infrastructure.Events;

namespace DomainService.EventHandlers
{
    public class UserEventHandler : IEventHandler<EntityCreateEvent<User>>,IEventHandler<EntityUpdateEvent<User>>,IEventHandler<EntityDeleteEvent<User>>
    {
        private IUserRepository _userRepository;
        public void Handle(EntityCreateEvent<User> eventObject,IUnitOfWork unitOfWork)
        {
            _userRepository = new UserRepository(unitOfWork);
            _userRepository.AddUser(eventObject.Entity);
        }

        public void Handle(EntityUpdateEvent<User> eventObject,IUnitOfWork unitOfWork)
        {
            _userRepository = new UserRepository(unitOfWork);
            _userRepository.UpdateCredential(eventObject.Entity);
        }

        public void Handle(EntityDeleteEvent<User> eventObject,IUnitOfWork unitOfWork)
        {
            _userRepository = new UserRepository(unitOfWork);
            _userRepository.DeleteCredential(eventObject.Entity.Id);
        }
    }

}
