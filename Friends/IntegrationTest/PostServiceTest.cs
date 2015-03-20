
using System;
using System.Linq;
using BusinessDomain.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.Model;

namespace IntegrationTest
{
    [TestClass]
    public class PostServiceTest
    {
        private UserService _userService;
        private PostService _postService;
        private LikeService _likeService;
        private string _userId = "ns82ugv6uwx";
        private string _postId = "nsweg7d2l8h";
        private string _commentId = "nsx8u7um5tt";
        private UserDTO _user;

        public PostServiceTest()
        {
            ServiceModelMapper.CreateMap();
            _userService = new UserService();
            _postService = new PostService();
            _likeService=new LikeService();
            _user = _userService.Get(_userId);
        }

        [TestMethod]
        public void AddPost()
        {
            var post = PostGenerator.CreateTextPost(_userId);
            var insertedPost = _postService.Post(post, _user);
            Assert.IsNotNull(insertedPost);
        }

        [TestMethod]
        public void AddComment()
        {

            var comment = PostGenerator.CreateComment(_postId, _userId);
            var insertedComment = _postService.Post(comment, _user);
            var post = _postService.Get(_postId, _user, PostType.PostText);
            Assert.IsNotNull(post);
            Assert.IsTrue(post.Comments.Count > 0);

        }

        [TestMethod]
        public void GetPosts()
        {
            var posts =
                _postService.Get(
                    new PostFetchRequest
                    {
                        AuthorId = _userId,
                        RecipientId = _userId,
                        PageNumber = 1,
                        RecordsPerPage = 10
                    }, _user);
            Assert.IsTrue(posts.Any());
        }

        [TestMethod]
        public void Like()
        {
           _likeService.Post(_postId,PostType.PostText, LikeType.Like, _user);
            _likeService.Post(_commentId,PostType.Comment, LikeType.Like, _user);
            var post = _postService.Get(_postId, _user,PostType.PostText);
            Assert.AreEqual(1,post.NumberOfLikes);
            var com = post.Comments.FirstOrDefault(c => c.Id == _commentId);
            Assert.AreEqual(1,com.NumberOfLikes);
        }

        [TestMethod]
        public void Dislike()
        {
            _likeService.Post(_postId, PostType.PostText, LikeType.Dislike, _user);
            _likeService.Post(_commentId, PostType.Comment, LikeType.Dislike, _user);
            var post = _postService.Get(_postId, _user, PostType.PostText);
            Assert.AreEqual(0, post.NumberOfLikes);
            Assert.AreEqual(1, post.NumberOfDislikes);
            var com = post.Comments.FirstOrDefault(c => c.Id == _commentId);
            Assert.AreEqual(0, com.NumberOfLikes);
            Assert.AreEqual(1, com.NumberOfDislikes);
        }
    }
}
