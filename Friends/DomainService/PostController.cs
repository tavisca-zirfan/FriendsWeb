using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Infrastructure.Data;
using BusinessDomain.DomainObjects;

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
        public BusinessDomain.DomainObjects.Post CreatePost(string authorId, string recipientId, string postMessage)
        {
            var post = new Post
            {
                Author = new User {Id = authorId},
                CreatedAt = DateTime.UtcNow,
                PostId = Guid.NewGuid().ToString(),
                PostMessage = postMessage,
                PostType = PostType.Post,
                Recipient = new User {Id = recipientId}
            };
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

        

        public bool RemovePost(string userId, string postId)
        {
            try
            {
                var post = PostRepository.GetPost(postId, PostType.Post, userId);
                PostRepository.DeletePost(post);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Post> GetPosts(string userId)
        {
            return PostRepository.GetPosts(PostType.Post, userId);
        }
    }
}
