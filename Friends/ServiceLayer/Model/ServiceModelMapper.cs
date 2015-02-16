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
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<Post, PostDTO>();
            Mapper.CreateMap<BusinessDomain.DomainObjects.Profile, ProfileDTO>();
        }
    }
}
