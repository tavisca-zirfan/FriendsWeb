﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace ServiceLayer.Model
{
    public class PostDTO
    {
        public string Id { get; set; }
        public ProfileThumbnailDTO Author { get; set; }
        public DateTime? CreatedAt { get; set; }
        public PostType PostType { get; set; }
        public IList<CommentDTO> Comments { get; set; }
        public IList<ProfileThumbnailDTO> Likes;
        public IList<ProfileThumbnailDTO> Recipients { get; set; }
        public IList<ProfileThumbnailDTO> Tags { get; set; }
        public IList<ProfileThumbnailDTO> Dislikes { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
    }
}
