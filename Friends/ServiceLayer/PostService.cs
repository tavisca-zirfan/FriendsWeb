using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BusinessDomain.DomainObjects;
using DomainService;
using Infrastructure.Container;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface IPostService
    {
        IEnumerable<PostDTO> Get(SearchFilter request, UserDTO authUser);
        PostDTO Get(string postId,UserDTO authUser,PostType postType);
        PostDTO Put(PostDTO request, UserDTO authUser);
        PostDTO Post(PostDTO request, UserDTO authUser);
        void Delete(string postId,UserDTO authUser);
    }

    public class PostService : IPostService,ILikeService
    {
        public IPostController PostController { get; set; }

        public PostService()
        {
            PostController = ObjectFactory.Resolve<IPostController>();
        }
        public IEnumerable<PostDTO> Get(SearchFilter request, UserDTO authUser)
        {
            var listOfTypes = new List<PostType> {PostType.PostText};
            
            request.FilterProperties.Add(new Filter{Name = "recipientid",Value = authUser.Id});
            request.FilterProperties.Add(new Filter { Name = "authorid", Value = authUser.Id });
            var posts = PostController.GetPosts(request, listOfTypes,authUser.ToBusinessModel());
            return posts.Select(Mapper.Map<PostDTO>);
        }

        public PostDTO Get(string postId, UserDTO authUser, PostType postType)
        {
            return Mapper.Map<PostDTO>(PostController.GetPost(postId,authUser.ToBusinessModel(), postType));
        }

        public PostDTO Post(PostDTO request, UserDTO authUser)
        {
            var post=PostController.CreatePost(request.ToBusinessModel(),authUser.ToBusinessModel());
            return Mapper.Map<PostDTO>(post);
        }

        public void Delete(string postId, UserDTO authUser)
        {
            PostController.RemovePost(postId, authUser.ToBusinessModel());
        }


        public PostDTO Put(PostDTO request, UserDTO authUser)
        {
            var post = PostController.UpdatePost(request.ToBusinessModel(), authUser.ToBusinessModel());
            return Mapper.Map<PostDTO>(post);
        }

        public bool Post(string postId, PostType postType, LikeType likeType, UserDTO authUser)
        {
            try
            {
                var user = authUser.ToBusinessModel();
                var post = PostController.GetPost(postId, user, postType);
                if (likeType == LikeType.Like)
                {
                    post.Like(user);
                }
                else
                {
                    post.Dislike(user);
                }
                post.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
