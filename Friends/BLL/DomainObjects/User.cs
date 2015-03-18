using System;
using System.Collections.Generic;
using System.Linq;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.UserEvents;
using Infrastructure.Container;
using Infrastructure.Events;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class User:EntityBase<string>
    {
        public string ChangedPassword { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public IEnumerable<Role> Roles { get; set; } 
        public int IsActive { get; set; }
        public DateTime LastSeen { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ImageUrl { get; set; }
        private IList<Profile> _friends; 
        public IList<Profile> Friends
        {
            get
            {
                if (_friends!=null)
                    return _friends;
                AddLoadEvent(new LoadFriendsEvent(Id, _friends));
                Load();
                return _friends;
            }
            set { _friends = value; }
        }

        public User()
        {
            
        }
        public void ChangePassword(string oldPassword,string newPassword)
        {
            if(this.Password==oldPassword)
                ChangePassword(newPassword);
        }
        public void ChangePassword(string newPassword)
        {
            this.ChangedPassword = newPassword;
            Update();
        }

        public void Update()
        {
            AddSaveEvent(new EntityUpdateEvent<User>(this));
            Save();
        }

        public void SendFriendRequest(User user)
        {
            if(IsFriendWith(user))
                return;
            AddSaveEvent(new AddFriendEvent(this.Id,user.Id));
            Save();
        }

        public bool IsFriendWith(User user)
        {
            return true;
            return Friends.Any(f => f.Id == user.Id);
        }
    }
    
}
