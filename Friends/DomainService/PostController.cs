﻿using System;
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
        public BusinessDomain.DomainObjects.Post CreatePost(string authorId, string recipientId, string postMessage)
        {
            var post = new TextPost
            {
                Author = new Profile {Id = authorId},
                CreatedAt = DateTime.UtcNow,
                Id = IdGenerator.GenerateId(),
                Message = postMessage,
                PostType = PostType.PostText,
                Recipients = new List<Profile>{new Profile {Id = recipientId}}
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

        public IEnumerable<Post> GetPosts(SearchFilter filter)
        {
            return PostRepository.GetPosts(filter);
        }
    }
}
