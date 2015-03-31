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
            Mapper.CreateMap<User, UserDTO>().ForMember(u => u.Friends, f => f.MapFrom(u=>u.Friends.Select(p=>p.Id))).ReverseMap();
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<Post, PostDTO>().Include<TextPost,TextPostDTO>().Include<Comment,CommentDTO>();
            Mapper.CreateMap<TextPost, TextPostDTO>();
            Mapper.CreateMap<Comment, CommentDTO>();
            Mapper.CreateMap<BusinessDomain.DomainObjects.Profile, ProfileDTO>().ForMember(p=>p.LastSeen,opt=>opt.MapFrom(src=>src.LastSeen.ToUniversalTime().ToString()));
            Mapper.CreateMap<BusinessDomain.DomainObjects.Profile, ProfileThumbnailDTO>();
            Mapper.CreateMap<Role, RolesDTO>().ReverseMap();
        }
    }

    public class CustomResolver : ValueResolver<User, List<string>>
    {
        protected override List<string> ResolveCore(User source)
        {
            return source.Friends.Select(p=>p.Id).ToList();
        }
    }
}
