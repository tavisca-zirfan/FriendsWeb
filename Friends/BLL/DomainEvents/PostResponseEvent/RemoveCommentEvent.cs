using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Events;

namespace BusinessDomain.DomainEvents.PostResponseEvent
{
    public class RemoveCommentEvent:EventBase
    {
        public string PostId { get; private set; }
        public string CommentId { get; private set; }
        public IEnumerable<string> PostIds { get; private set; }
        public IEnumerable<string> CommentIds { get; private set; }

        public RemoveCommentEvent(string postId = null, string commentId = null, IEnumerable<string> postIds = null, IEnumerable<string> commentIds = null)
        {
            PostId = postId;
            CommentId = commentId;
            PostIds = postIds;
            CommentIds = commentIds;
        }
        public override void Raise()
        {
            Dispatcher.Dispatch(this);
        }
    }
}
