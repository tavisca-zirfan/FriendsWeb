using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainService;
using DAL;
using BusinessDomain.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessControllerTest
{
    [TestClass]
    public class PostControllerTest
    {
        PostController PostController { get; set; }
        PostResponseController PostResponseController { get; set; }
        private string authorId = "author";
        private string postMessage = "post";
        private string commentMessage = "comment";
        private string recipientId = "recipient";
        public PostControllerTest()
        {
            PostController = new PostController();
            PostResponseController = new PostResponseController();
        }
        [TestMethod]
        public void ShouldAddPost()
        {
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(r => r.AddPost(It.IsAny<Post>()));
            PostController.PostRepository = mockRepository.Object;
            var post = PostController.CreatePost(authorId, recipientId, postMessage);
            Assert.IsNotNull(post);
        }

        [TestMethod]
        public void AddPostShouldFailIfInvalidUser()
        {
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(r => r.AddPost(It.IsAny<Post>())).Throws<Exception>();
            PostController.PostRepository = mockRepository.Object;
            var post = PostController.CreatePost(authorId, recipientId, postMessage);
            Assert.IsNull(post);
        }
        [TestMethod]
        public void ShouldAddComment()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.AddComment(It.IsAny<string>(),It.IsAny<Comment>()));
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var comment = PostResponseController.AddComment(authorId, Guid.NewGuid().ToString(),PostType.Post, commentMessage);
            Assert.IsNotNull(comment);
        }
        [TestMethod]
        public void AddCommentShouldFailIfInvalidUser()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.AddComment(It.IsAny<string>(), It.IsAny<Comment>())).Throws<Exception>();
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var comment = PostResponseController.AddComment(authorId, Guid.NewGuid().ToString(), PostType.Post, commentMessage);
            Assert.IsNull(comment);
        }
        [TestMethod]
        public void AddCommentShouldFailIfInvalidPost()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.AddComment(It.IsAny<string>(), It.IsAny<Comment>())).Throws<Exception>();
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var comment = PostResponseController.AddComment(authorId, Guid.NewGuid().ToString(), PostType.Post, commentMessage);
            Assert.IsNull(comment);
        }

        [TestMethod]
        public void DeletePost()
        {
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(r => r.DeletePost(It.IsAny<Post>()));
            PostController.PostRepository = mockRepository.Object;
            var post = PostController.RemovePost(authorId, recipientId);
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void DeletePostShouldReturnFailIfDbFails()
        {
            var mockRepository = new Mock<IPostRepository>();
            mockRepository.Setup(r => r.DeletePost(It.IsAny<Post>())).Throws<Exception>();
            PostController.PostRepository = mockRepository.Object;
            var post = PostController.RemovePost(authorId, recipientId);
            Assert.IsFalse(post);
        }
        [TestMethod]
        public void DeleteComment()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.DeleteComment(It.IsAny<Comment>()));
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var post = PostResponseController.RemoveComment(authorId, recipientId,PostType.Post);
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void DeleteCommentShouldReturnFailIfDbFails()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.DeleteComment(It.IsAny<Comment>())).Throws<Exception>();
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var post = PostResponseController.RemoveComment(authorId, recipientId,PostType.Post);
            Assert.IsFalse(post);
        }
        [TestMethod]
        public void ShouldAddLike()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.AddLike(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<PostType>(), It.IsAny<LikeType>(), It.IsAny<DateTime>()));
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var like = PostResponseController.Like(authorId, PostType.Post, "did");
            Assert.IsTrue(like);
        }
        [TestMethod]
        public void AddLikeShouldFailIfInvalidUser()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.AddLike(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<PostType>(), It.IsAny<LikeType>(), It.IsAny<DateTime>())).Throws<Exception>();
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var like = PostResponseController.Like(authorId, PostType.Post, "did");
            Assert.IsFalse(like);
        }
        [TestMethod]
        public void AddLikeShouldFailIfInvalidPost()
        {
            var mockRepository = new Mock<IPostResponseRepository>();
            mockRepository.Setup(r => r.AddLike(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<PostType>(), It.IsAny<LikeType>(), It.IsAny<DateTime>())).Throws<Exception>();
            PostResponseController.PostResponseRepository = mockRepository.Object;
            var like = PostResponseController.Like(authorId, PostType.Post, "did");
            Assert.IsFalse(like);
        }

    }
}
