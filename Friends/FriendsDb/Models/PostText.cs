using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class PostText
    {
        public int Id { get; set; }
        public string PostId { get; set; }
        public string Text { get; set; }
        public virtual Post Post { get; set; }
    }
}
