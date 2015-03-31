using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiceLayer;
using ServiceLayer.Model;

namespace Friends.Controllers
{
    public class ProfileController : BaseApiController
    {
        public IProfileService ProfileService { get; set; }

        public ProfileController()
        {
            ProfileService=new ProfileService();
        }
        ProfileDTO Get(string userId)
        {
            return ProfileService.Get(userId, UserData);
        }

        PagedList<ProfileDTO> Get(List<string> userIds)
        {
            return new PagedList<ProfileDTO> {Items = ProfileService.Get(userIds, UserData)};
        }

        [HttpGet]
        public PagedList<ProfileDTO> GetFriends()
        {
            return Get(UserData.Friends.ToList());
        } 

        public void Update(ProfileDTO profile)
        {
            ProfileService.Put(profile,UserData);
        }
    }
}
