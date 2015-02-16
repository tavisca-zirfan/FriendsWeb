using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.Model
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
