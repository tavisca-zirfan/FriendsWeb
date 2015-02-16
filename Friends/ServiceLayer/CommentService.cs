using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public interface ICommentService
    {
        IEnumerable<CommentDTO> Get(string postId);
        void Post(CommentDTO request);
        void Delete(string commentId);
    }
    class CommentService
    {
    }
}
