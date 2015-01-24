using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using DAL;
using Infrastructure.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbProviderTest
{
    [TestClass]
    public class PostRepositoryTest
    {
        [TestMethod]
        public void GetPost()
        {
            var r = new PostRepository();
            var p = r.GetPost("p1", PostType.Post, "zbi");
           Assert.IsNotNull(p);
        }
    }
}
