using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public string Post1 { get; set; }
        public string Author { get; set; }
        public string Recipient { get; set; }
        public System.DateTime Time { get; set; }
        public virtual UserCredential UserCredential { get; set; }
        public virtual UserCredential UserCredential1 { get; set; }
    }
}
