
using System.Linq;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.UserEvents;
using BusinessDomain.DomainObjects;
using DAL;
using Infrastructure.Container;
using Infrastructure.Data;
using Infrastructure.Events;

namespace DomainService.EventHandlers
{
    public class UserEventHandler : IEventHandler<EntityCreateEvent<User>>,IEventHandler<EntityUpdateEvent<User>>,IEventHandler<EntityDeleteEvent<User>>,
        IEventHandler<LoadFriendsEvent>,IEventHandler<ChangePasswordEvent>
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public UserEventHandler()
        {
            _unitOfWork = ObjectFactory.Resolve<IUnitOfWork>();
            _userRepository = new UserRepository(_unitOfWork);
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

        public void Handle(LoadFriendsEvent eventObject)
        {
            eventObject.Friends = _userRepository.GetFriends(eventObject.UserId).ToList();
        }

        public void Handle(ChangePasswordEvent eventObject)
        {
            
        }
    }

}
