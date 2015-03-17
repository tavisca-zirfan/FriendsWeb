using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Infrastructure.Common;
using Infrastructure.Data;
using BusinessDomain.DomainObjects;
using Infrastructure.Model;

namespace DomainService
{
    public class PostController:IPostController
    {
        public IPostRepository PostRepository { get; set; }
        public IPostResponseRepository PostResponseRepository { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public PostController()
        {
            UnitOfWork = new UnitOfWork();
            PostRepository=new PostRepository(UnitOfWork);
            PostResponseRepository=new PostResponseRepository(UnitOfWork);

        }
        public BusinessDomain.DomainObjects.Post CreatePost(Post post, User authUser)
        {
            try
            {
                PostRepository.AddPost(post);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return null;
            }
            return post;
        }



        public bool RemovePost(string postId, User authUser)
        {
            try
            {
                var post = PostRepository.GetPost(postId, PostType.PostText);
                PostRepository.DeletePost(post);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Post> GetPosts(SearchFilter filter, IEnumerable<PostType> types, User authUser)
        {
            return PostRepository.GetPosts(filter,types);
        }


        public Post GetPost(string postId, User authUser, PostType postType)
        {
            return PostRepository.GetPost(postId, postType);
        }


        public Post UpdatePost(Post post, User authUser)
        {
            throw new NotImplementedException();
        }
    }
}
