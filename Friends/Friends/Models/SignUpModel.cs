using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friends.Models
{
    public class SignUpModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int DateDOB { get; set; }
        public int MonthDOB { get; set; }
        public int YearDOB { get; set; }
    }
}