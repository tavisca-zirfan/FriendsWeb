using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Model
{
    public class Post
    {
        public string PostId { get; set; }
        public string PostMessage { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; }  
        public int Likes { get; set; }
        public PostType PostType;
    }
}
