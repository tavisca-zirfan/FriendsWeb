using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DomainService;
using Infrastructure.Container;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IProfileService
    {
        void Put(ProfileDTO request, UserDTO authUser);
        ProfileDTO Get(string userId,UserDTO authUser);
        List<ProfileDTO> Get(List<string> ids,UserDTO authUser);
        List<ProfileDTO> Get(SearchFilter filters, UserDTO authUser);
    }
    public class ProfileService:IProfileService
    {
        public IUserController UserController { get; set; }

        public ProfileService()
        {
            UserController = ObjectFactory.Resolve<IUserController>();
        }

        public void Put(ProfileDTO request, UserDTO authUser)
        {
            var profile = Mapper.Map<BusinessDomain.DomainObjects.Profile>(request);
            UserController.UpdateProfile(profile,authUser.ToBusinessModel());
        }

        public ProfileDTO Get(string userId, UserDTO authUser)
        {
            var profile = UserController.GetProfile(userId,authUser.ToBusinessModel());
            return Mapper.Map<ProfileDTO>(profile);
        }

        public List<ProfileDTO> Get(List<string> ids, UserDTO authUser)
        {
            var profiles = UserController.GetProfiles(ids,authUser.ToBusinessModel());
            return profiles.Select(Mapper.Map<ProfileDTO>).ToList();
        }

        public List<ProfileDTO> Get(SearchFilter filters, UserDTO authUser)
        {
            return UserController.GetProfiles(filters,authUser.ToBusinessModel()).Select(Mapper.Map<ProfileDTO>).ToList();
        }
    }
}
