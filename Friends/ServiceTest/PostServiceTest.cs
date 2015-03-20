using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLayer;
using ServiceLayer.Model;

namespace ServiceTest
{
    [TestClass]
    public class PostServiceTest
    {
        private UserService _userService;
        private PostService _postService;
        private string _userId = "ns82ugv6uwx";
        private UserDTO _user;

        public PostServiceTest()
        {
            ServiceModelMapper.CreateMap();
            _userService = new UserService();
            _postService = new PostService();
            _user = _userService.Get(_userId);
        }
    }
}
