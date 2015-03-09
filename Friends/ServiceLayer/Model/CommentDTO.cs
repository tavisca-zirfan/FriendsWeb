

using System;
using BusinessDomain.DomainObjects;

namespace ServiceLayer.Model
{
    public class CommentDTO
    {
        public string Id { get; set; }
        public string CommentMessage { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime? CommentedAt { get; set; }
        public UserDTO CommentedBy { get; set; }
        public PostType? PostType { get; set; }
        public string PostId { get; set; }
    }
}
