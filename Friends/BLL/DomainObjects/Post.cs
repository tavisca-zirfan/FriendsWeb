using System;
using System.Collections.Generic;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class Post:EntityBase<string>
    {
        public string PostId { get; set; }
        public string PostMessage { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; }  
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public PostType PostType;
    }
}
