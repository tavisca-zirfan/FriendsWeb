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
                //UserRepository.DeleteCredential(userId);
                //UnitOfWork.Commit();
                //var user = UserGenerator.CreateUserForCredential("test@test.com", "qwerty");
                //user.Id = userId;
                //UserRepository.AddUser(user);
                //UnitOfWork.Commit();
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
        [TestMethod]
        public void GetPost()
        {
            var r = new PostRepository();
            var p = r.GetPost(postId, PostType.PostText, "zbi");
           Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddPost()
        {
            try
            {
                var textpost = PostGenerator.CreateTextPost(postId, userId);
                PostRepository.AddPost(textpost);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    
}
