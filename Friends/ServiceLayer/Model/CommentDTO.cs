﻿

using System;
using BusinessDomain.DomainObjects;

namespace ServiceLayer.Model
{
    public class CommentDTO:PostDTO
    {
        public string CommentMessage { get; set; }
    }
}
