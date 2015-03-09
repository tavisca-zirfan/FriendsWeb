using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BusinessDomain.DomainObjects;

namespace ServiceLayer.Model
{
    public class ServiceModelMapper
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<User, UserDTO>().ReverseMap();
            //Mapper.CreateMap<UserDTO, User>();
            Mapper.CreateMap<Comment, CommentDTO>().ReverseMap();
            Mapper.CreateMap<Post, PostDTO>().ReverseMap();
            Mapper.CreateMap<BusinessDomain.DomainObjects.Profile, ProfileDTO>().ReverseMap();
            Mapper.CreateMap<Role, RolesDTO>().ReverseMap();
        }
    }
}
