using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Model
{
    public class ProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string About { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> LocationId { get; set; }
    }
}
