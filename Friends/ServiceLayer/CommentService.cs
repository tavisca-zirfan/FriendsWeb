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
        IEnumerable<CommentDTO> Get(string postId, UserDTO authUser);
        void Post(CommentDTO request, UserDTO authUser);
        void Delete(string commentId, UserDTO authUser);
    }
    class CommentService
    {
    }
}
