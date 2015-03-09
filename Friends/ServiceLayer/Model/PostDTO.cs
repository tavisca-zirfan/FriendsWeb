using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace ServiceLayer.Model
{
    public class PostDTO
    {
        public string Id { get; set; }
        public string PostMessage { get; set; }
        public UserDTO Author { get; set; }
        public UserDTO Recipient { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IList<CommentDTO> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public PostType PostType;
    }
}
