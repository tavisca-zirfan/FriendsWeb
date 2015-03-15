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
        IEnumerable<PostDTO> Get(PostFetchRequest request, UserDTO authUser);
        PostDTO Get(string postId,UserDTO user,PostType postType);
        void Post(PostDTO request, UserDTO authUser);
        void Delete(string postId,UserDTO user);
    }

    public class PostService : IPostService
    {
        public IPostController PostController { get; set; }

        public PostService()
        {
            PostController = ObjectFactory.Resolve<IPostController>();
        }
        public IEnumerable<PostDTO> Get(PostFetchRequest request, UserDTO authUser)
        {
            var listOfTypes = new List<PostType> {PostType.PostText};
            var filter = new SearchFilter
            {
                Order = OrderType.Ascending,
                PageNumber = request.PageNumber,
                RecordsPerPage = request.RecordsPerPage
            };
            filter.FilterProperties.Add("authorid",request.AuthorId);
            filter.FilterProperties.Add("recipientid",request.RecipientId);
            var posts = PostController.GetPosts(filter, listOfTypes,authUser.ToBusinessModel());
            return posts.Select(Mapper.Map<PostDTO>);
        }

        public PostDTO Get(string postId, UserDTO authUser, PostType postType)
        {
            return Mapper.Map<PostDTO>(PostController.GetPost(postId,authUser.ToBusinessModel(), postType));
        }

        public void Post(PostDTO request, UserDTO authUser)
        {
            PostController.CreatePost(Mapper.Map<Post>(request),authUser.ToBusinessModel());
        }

        public void Delete(string postId, UserDTO authUser)
        {
            PostController.RemovePost(postId, authUser.ToBusinessModel());
        }
    }

}
