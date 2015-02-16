using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IProfileService
    {
        void Put(ProfileDTO request);
        ProfileDTO Get(string userId);
    }
    class ProfileService
    {
    }
}
