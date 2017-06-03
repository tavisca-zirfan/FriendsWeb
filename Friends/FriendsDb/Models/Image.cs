using System;
using System.Collections.Generic;

namespace FriendsDb.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string ImageId { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> UploadTime { get; set; }
        public virtual UserCredential UserCredential { get; set; }
    }
}
