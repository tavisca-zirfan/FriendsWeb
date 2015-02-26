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
        List<ProfileDTO> Get(List<string> ids);
        List<ProfileDTO> Get(SearchFilter filters);
    }
    public class ProfileService:IProfileService
    {
        public void Put(ProfileDTO request)
        {
            throw new NotImplementedException();
        }

        public ProfileDTO Get(string userId)
        {
            throw new NotImplementedException();
        }

        public List<ProfileDTO> Get(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public List<ProfileDTO> Get(SearchFilter filters)
        {
            throw new NotImplementedException();
        }
    }
}
