﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.PostEvent
{
    public class AddPostRecipient:EventBase
    {
        public Post Post { get; set; }
        public IEnumerable<string> UserIds { get; set; } 
        public AddPostRecipient(Post post,IEnumerable<string> userIds)
        {
            Post = post;
            UserIds = userIds;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}