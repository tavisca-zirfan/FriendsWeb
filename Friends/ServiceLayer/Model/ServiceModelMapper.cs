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
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<Post, PostDTO>().Include<TextPost,TextPostDTO>().Include<Comment,CommentDTO>();
            Mapper.CreateMap<TextPost, TextPostDTO>();
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<BusinessDomain.DomainObjects.Profile, ProfileDTO>();
            Mapper.CreateMap<BusinessDomain.DomainObjects.Profile, ProfileThumbnailDTO>();
            Mapper.CreateMap<Role, RolesDTO>().ReverseMap();
        }
    }
}
