using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.EventHandlers
{
    public class UserAddHandler<T>:IEventHandler<User>
    {
        IUserRepository UserRepository { get; set; }
        public void Handle(User eventObject)
        {
            throw new NotImplementedException();
        }
    }
}
