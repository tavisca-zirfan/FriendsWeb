using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public static class Translator
    {
        public static User ToBusinessModel(this UserDTO userDto,User user=null)
        {
            if(user==null)
                user=new User();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.DOB = userDto.DOB;
            user.Email = userDto.Email;
            user.Gender = userDto.Gender;
            if (string.IsNullOrEmpty(user.Id))
            {
                user.Id = userDto.Id;
            }
            if(userDto.Friends!=null)
                user.Friends = userDto.Friends.Select(f => new Profile {Id = f}).ToList();
            user.Password = userDto.Password;
            user.Roles = userDto.Roles.Select(r => new Role {RoleId = r.Id, RoleName = r.RoleName});
            return user;
        }

        public static Profile ToBusinessModel(this ProfileDTO profileDto,Profile profile)
        {
            if(profile==null)
                profile =new Profile();
            if (string.IsNullOrEmpty(profile.Id))
            {
                profile.Id = profileDto.Id;
            }
            profile.About = profileDto.About;
            profile.DOB = profileDto.DOB;
            profile.FirstName = profileDto.FirstName;
            profile.LastName = profileDto.LastName;
            profile.Gender = profileDto.Gender;
            profile.ImageUrl = profileDto.ImageUrl;
            profile.LocationId = profileDto.LocationId;
            profile.StatusId = profileDto.StatusId;
            return profile;
        }

        public static Post ToBusinessModel(this PostDTO postDto, Post post=null)
        {
            PostType postType;
            if (Enum.TryParse(postDto.PostType, out postType))
            {
                switch (postType)
                {
                    case PostType.PostText:
                    post = ToPostBusinessModel((TextPostDTO) postDto, (TextPost) post);
                    break;
                    case PostType.Comment:
                    post = ToPostBusinessModel((CommentDTO) postDto, (Comment) post);
                    break;
                }
            }
            if (post != null)
            {
                if (!string.IsNullOrEmpty(postDto.Id))
                    post.Id = postDto.Id;
                post.Author = new Profile{Id=postDto.Author.Id};
                post.Tags = postDto.Tags.Select(t => new Profile {Id = t.Id}).ToList();
                post.Recipients = postDto.Recipients.Select(r => new Profile {Id = r.Id}).ToList();
            }
            return post;
        }

        private static TextPost ToPostBusinessModel(TextPostDTO postDto, TextPost post=null)
        {
            if(post==null)
                post = new TextPost();
            post.Message = postDto.Message;
            if (string.IsNullOrEmpty(post.Id))
            {
                post.Id = post.Id;
            }
            return post;
        }

        private static Comment ToPostBusinessModel(CommentDTO postDto, Comment post = null)
        {
            if (post == null)
                post = new Comment();
            post.CommentMessage = postDto.CommentMessage;
            post.ForPostId = postDto.ForPostId;
            if (string.IsNullOrEmpty(post.Id))
            {
                post.Id = post.Id;
            }
            return post;
        }
    }
}
