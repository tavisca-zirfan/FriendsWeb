using System;
using System.Collections.Generic;
using System.Linq;
using BusinessDomain.DomainEvents.Common;
using BusinessDomain.DomainEvents.UserEvents;
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

        public void ChangePassword(string oldPassword,string newPassword)
        {
            if(this.Password==oldPassword)
                ChangePassword(newPassword);
        }
        public void ChangePassword(string newPassword)
        {
            this.ChangedPassword = newPassword;
            AddEvent(new ChangePasswordEvent(this));
        }

        public void AddRole(Role role)
        {
            if (Roles.Count(r => r.RoleId == role.RoleId) > 0)
            {
                
            }
        }
    }
    
}
