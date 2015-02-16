using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessDomain.DomainObjects
{
    public class Profile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }

        public int Age
        {
            get
            {
                DateTime zeroTime = new DateTime(1, 1, 1);
                return (zeroTime + (DateTime.Now.AddDays(1) - DOB)).Year - 1;
            }
        }

        public string Gender { get; set; }
        public string About { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public System.DateTime LastSeen { get; set; }
    }
}
