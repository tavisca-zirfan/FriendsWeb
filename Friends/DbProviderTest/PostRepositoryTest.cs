using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DAL;
using BusinessDomain.DomainObjects;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbProviderTest
{
    [TestClass]
    public class PostRepositoryTest
    {
        private string postId = "pid1";
        private string postId2 = "pid2";
        private string commentId = "cid1";
        private string commentId2 = "cid2";
        private static string userId = "uid1";
        private Post post;
        private static UserRepository UserRepository { get; set; }
        private static PostResponseRepository PostResponseRepository { get; set; }
        private static PostRepository PostRepository { get; set; }
        private static IUnitOfWork UnitOfWork { get; set; }

        public PostRepositoryTest()
        {
            UnitOfWork = new UnitOfWork();
            PostResponseRepository = new PostResponseRepository(UnitOfWork);
            PostRepository = new PostRepository(UnitOfWork);
            UserRepository = new UserRepository(UnitOfWork);
        }
        [ClassInitialize]
        public static void ClassSetUp(TestContext context)
        {
            try
            {
                UserRepository.DeleteCredential(userId);
                UnitOfWork.Commit();
                var user = UserGenerator.CreateUserForCredential("test@test.com", "qwerty");
                user.Id = userId;
                UserRepository.AddUser(user);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Refresh();
                
            }
        }

        [ClassCleanup]
        public static void ClassDispose()
        {
            try
            {
                UnitOfWork = new UnitOfWork();
                PostResponseRepository = new PostResponseRepository(UnitOfWork);
                PostRepository = new PostRepository(UnitOfWork);
                UserRepository = new UserRepository(UnitOfWork);
                UserRepository.DeleteCredential(userId);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Refresh();
            }
        }

        [TestCleanup]
        public void TestDispose()
        {
            try
            {
                UnitOfWork = new UnitOfWork();
                PostResponseRepository = new PostResponseRepository(UnitOfWork);
                PostRepository = new PostRepository(UnitOfWork);
                UserRepository = new UserRepository(UnitOfWork);
                var postToBeDeleted = PostRepository.GetPost(postId, PostType.PostText.ToString());
                if(postToBeDeleted!=null)
                PostRepository.DeletePost(postToBeDeleted);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Refresh();
            }
        }

        [TestInitialize]
        public void TestStartUp()
        {
            try
            {
                UnitOfWork = new UnitOfWork();
                PostResponseRepository = new PostResponseRepository(UnitOfWork);
                PostRepository = new PostRepository(UnitOfWork);
                UserRepository = new UserRepository(UnitOfWork);
                var postToBeDeleted = PostRepository.GetPost(postId, PostType.PostText.ToString());
                if(postToBeDeleted!=null)
                PostRepository.DeletePost(postToBeDeleted);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Refresh();
            }
        }

        [TestMethod]
        public void GetPost()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            var p = PostRepository.GetPost(postId, PostType.PostText.ToString());
           Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddPost()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            var p = PostRepository.GetPost(postId, PostType.PostText.ToString());
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddComment()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            var comment = PostGenerator.CreateComment(commentId, postId, userId);
            PostRepository.AddPost(comment);
            UnitOfWork.Commit();
            var p = PostRepository.GetPost(postId, PostType.PostText.ToString());
            Assert.IsNotNull(p);
            Assert.AreEqual(1,p.Comments.Count);
            Assert.AreEqual(1,p.Comments[0].Tags.Count);
            Assert.AreEqual(1, p.Comments[0].Recipients.Count);
        }

        [TestMethod]
        public void AddLike()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            PostRepository.AddLike(textpost.Id,userId,LikeType.Like);
            UnitOfWork.Commit();
            var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(1,myPost.Likes.Count);
        }

        [TestMethod]
        public void AddDislike()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            PostRepository.AddLike(textpost.Id, userId, LikeType.Dislike);
            UnitOfWork.Commit();
            var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(1, myPost.Dislikes.Count);
        }

        [TestMethod]
        public void AddLikeAndDislike()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            PostRepository.AddLike(textpost.Id, userId, LikeType.Like);
            PostRepository.AddLike(textpost.Id, userId, LikeType.Dislike);
            UnitOfWork.Commit();
            var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(1, myPost.Likes.Count);
        }

        [TestMethod]
        public void AddTag()
        {

            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            PostRepository.AddTag(textpost.Id, userId);
            UnitOfWork.Commit();
            var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(2, myPost.Tags.Count);
        }

        [TestMethod]
        public void RemoveTag()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            PostRepository.AddTag(textpost.Id, userId);
            UnitOfWork.Commit();
            var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(2, myPost.Tags.Count);
            PostRepository.RemoveTag(textpost.Id, userId);
            UnitOfWork.Commit();
            myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(1, myPost.Tags.Count);
        }

        [TestMethod]
        public void AddRecipient()
        {
            
                var textpost = PostGenerator.CreateTextPost(postId, userId);
                PostRepository.AddPost(textpost);
                UnitOfWork.Commit();
                PostRepository.AddRecipient(textpost.Id, userId);
                UnitOfWork.Commit();
                var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
                Assert.AreEqual(2, myPost.Recipients.Count);
            
        }

        [TestMethod]
        public void RemoveRecipient()
        {
            var textpost = PostGenerator.CreateTextPost(postId, userId);
            PostRepository.AddPost(textpost);
            UnitOfWork.Commit();
            PostRepository.AddRecipient(textpost.Id, userId);
            UnitOfWork.Commit();
            var myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(2, myPost.Recipients.Count);
            PostRepository.RemoveRecipient(textpost.Id, userId);
            UnitOfWork.Commit();
            myPost = PostRepository.GetPost(textpost.Id, PostType.PostText.ToString());
            Assert.AreEqual(1, myPost.Recipients.Count);
        }


    }

    
}
