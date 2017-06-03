﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Model
{
    public class User
    {
        public string UserId { get; set; }
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
    }
}
