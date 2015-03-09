using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DAL;
using BusinessDomain.DomainObjects;
using Infrastructure.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbProviderTest
{

    [TestClass]
    public class PostResponseRepositoryTest
    {
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

        [TestInitialize]
        public void SetUp()
        {
            try
            {
                UnitOfWork = new UnitOfWork();
                PostResponseRepository = new PostResponseRepository(UnitOfWork);
                PostRepository = new PostRepository(UnitOfWork);
                UserRepository = new UserRepository(UnitOfWork);
                //post = PostGenerator.CreatePost(postId, userId);
                //PostRepository.AddPost(post);
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
                //PostResponseRepository.RemoveLike(userId, commentId, PostType.Comment, LikeType.Like);
                //PostResponseRepository.RemoveLike(userId, commentId2, PostType.Comment, LikeType.Like);
                //PostResponseRepository.RemoveLike(userId, postId, PostType.Post, LikeType.Like);
                //PostResponseRepository.RemoveLike(userId, postId2, PostType.Post, LikeType.Like);
                //PostResponseRepository.DeleteComment(post.PostId, PostType.Post);
                //PostRepository.DeletePost(PostGenerator.CreatePost(postId2,userId));
                //PostRepository.DeletePost(PostGenerator.CreatePost(postId,userId));
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                UnitOfWork.Refresh();
            }
        }

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

        public PostResponseRepositoryTest()
        {
            UnitOfWork = new UnitOfWork();
            PostResponseRepository = new PostResponseRepository(UnitOfWork);
            PostRepository = new PostRepository(UnitOfWork);
            UserRepository = new UserRepository(UnitOfWork);
        }

        [TestMethod]
        public void AddComment()
        {

            var comment = PostGenerator.CreateComment(commentId, PostType.PostText, userId);
            PostResponseRepository.AddComment(postId, comment);
            UnitOfWork.Commit();
            var curPost = PostRepository.GetPost(postId, PostType.PostText, userId);
            var curComment = curPost.Comments.FirstOrDefault(c => c.Id == commentId);
            Assert.IsNotNull(curComment);
            PostResponseRepository.DeleteComment(curComment);
            UnitOfWork.Commit();
        }

        [TestMethod]
        public void ShouldNotAddCommentIfUserNotExist()
        {

            var comment = PostGenerator.CreateComment(commentId, PostType.PostText, userId);
            try
            {
                comment.CommentedBy.Id = "invalid";
                PostResponseRepository.AddComment(postId, comment);
                UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                UnitOfWork.Refresh();
            }
        }

        [TestMethod]
        public void DeleteCommentByPassingCommentInfo()
        {
            var comment = PostGenerator.CreateComment(commentId, PostType.PostText, userId);
            var comment2 = PostGenerator.CreateComment(commentId2, PostType.PostText, userId);
            PostResponseRepository.AddComment(postId, comment);
            PostResponseRepository.AddComment(postId, comment2);
            UnitOfWork.Commit();
            PostResponseRepository.DeleteComment(comment);
            UnitOfWork.Commit();
            var curPost = PostRepository.GetPost(postId, PostType.PostText);
            var curComment = curPost.Comments.FirstOrDefault(c => c.Id == commentId);
            var curComment2 = curPost.Comments.FirstOrDefault(c => c.Id == commentId2);
            Assert.IsNull(curComment);
            Assert.IsNotNull(curComment2);
        }

        [TestMethod]
        public void DeleteCommentByPassingPostInfo()
        {

            var comment = PostGenerator.CreateComment(commentId, PostType.PostText, userId);
            var comment2 = PostGenerator.CreateComment(commentId2, PostType.PostText, userId);
            PostResponseRepository.AddComment(postId, comment);
            PostResponseRepository.AddComment(postId, comment2);
            UnitOfWork.Commit();
            var list = PostResponseRepository.DeleteComment(postId, PostType.PostText);
            UnitOfWork.Commit();
            Assert.AreEqual(list.Count(), 2);
            var curPost = PostRepository.GetPost(postId, PostType.PostText);
            var curComment = curPost.Comments;
            Assert.AreEqual(curComment.Count(), 0);
        }

        [TestMethod]
        public void AddLikes()
        {

            PostResponseRepository.AddLike(userId, "lt1", postId, PostType.PostText, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt2", postId, PostType.PostText, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt3", postId, PostType.PostText, LikeType.Dislike, DateTime.Now);
            var comment = PostGenerator.CreateComment(commentId, PostType.PostText, userId);
            var comment2 = PostGenerator.CreateComment(commentId2, PostType.PostText, userId);
            PostResponseRepository.AddComment(postId, comment);
            PostResponseRepository.AddComment(postId, comment2);
            PostResponseRepository.AddLike(userId, "lt4", commentId, PostType.Comment, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt5", commentId, PostType.Comment, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt6", commentId2, PostType.Comment, LikeType.Dislike, DateTime.Now);
            UnitOfWork.Commit();
            var curPost = PostRepository.GetPost(postId, PostType.PostText, userId);
            Assert.AreEqual(curPost.Likes, 2);
            Assert.AreEqual(1, curPost.Dislikes);
            Assert.AreEqual(curPost.Comments.FirstOrDefault(c => c.Id == commentId).Likes, 2);
            Assert.AreEqual(curPost.Comments.FirstOrDefault(c => c.Id == commentId2).Dislikes, 1);
        }

        [TestMethod]
        public void RemoveLikes()
        {

            PostResponseRepository.AddLike(userId, "lt1", postId, PostType.PostText, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt2", postId, PostType.PostText, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt3", postId, PostType.PostText, LikeType.Dislike, DateTime.Now);
            var comment = PostGenerator.CreateComment(commentId, PostType.PostText, userId);
            var comment2 = PostGenerator.CreateComment(commentId2, PostType.PostText, userId);
            PostResponseRepository.AddComment(postId, comment);
            PostResponseRepository.AddComment(postId, comment2);
            PostResponseRepository.AddLike(userId, "lt4", commentId, PostType.Comment, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt5", commentId, PostType.Comment, LikeType.Like, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt6", commentId2, PostType.Comment, LikeType.Dislike, DateTime.Now);
            PostResponseRepository.AddLike(userId, "lt7", commentId, PostType.Comment, LikeType.Dislike, DateTime.Now);
            UnitOfWork.Commit();
            var curPost = PostRepository.GetPost(postId, PostType.PostText, userId);
            Assert.AreEqual(curPost.Likes, 2);
            Assert.AreEqual(1, curPost.Dislikes);
            Assert.AreEqual(curPost.Comments.FirstOrDefault(c => c.Id == commentId).Likes, 2);
            Assert.AreEqual(curPost.Comments.FirstOrDefault(c => c.Id == commentId2).Dislikes, 1);
            PostResponseRepository.RemoveLike(userId,commentId,PostType.Comment,LikeType.Like);
            UnitOfWork.Commit();
            curPost = PostRepository.GetPost(postId, PostType.PostText, userId);
            Assert.AreEqual(curPost.Likes, 2);
            Assert.AreEqual(1, curPost.Dislikes);
            Assert.AreEqual(curPost.Comments.FirstOrDefault(c => c.Id == commentId).Likes, 0);
            Assert.AreEqual(curPost.Comments.FirstOrDefault(c => c.Id == commentId).Dislikes, 1);
        }



    }
}

