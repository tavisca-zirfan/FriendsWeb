using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessControllerTest
{
    [TestClass]
    public class PostControllerTest
    {
        PostController PostController { get; set; }
        private string authorId = "author";
        private string postMessage = "post";
        private string commentMessage = "comment";
        private string recipientId = "recipient";
        public PostControllerTest()
        {
            PostController = new PostController();
        }
        [TestMethod]
        public void ShouldAddPost()
        {
            var post = PostController.CreatePost(authorId, recipientId, postMessage);
            Assert.IsNotNull(post);
        }
    }
}
