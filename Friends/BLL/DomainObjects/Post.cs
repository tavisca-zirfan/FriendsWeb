using System;
using System.Collections.Generic;
using Infrastructure.Common;
using Infrastructure.Model;

namespace BusinessDomain.DomainObjects
{
    public class Post:EntityBase<string>,ILikable
    {
        public string PostId { get; set; }
        public string PostMessage { get; set; }
        public User Author { get; set; }
        public User Recipient { get; set; }
        public List<User> Recipients { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IEnumerable<Comment> Comments { get; set; }  
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public PostType PostType;

        public void Like(string userId)
        {
            throw new NotImplementedException();
        }

        public void Dislike(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
