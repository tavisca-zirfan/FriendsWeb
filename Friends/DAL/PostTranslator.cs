using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using FriendsDb.Models;
using Model = Infrastructure.Model;
using Post = FriendsDb.Models.Post;

namespace DAL
{
    public static class PostTranslator
    {
        public static void ToDbModel(this Model.Post post, Post dbPost)
        {
            dbPost.Pid = post.PostId;
            dbPost.PostMessage = post.PostMessage;
            if (post.Author != null)
                dbPost.Author = post.Author.UserId;
            if (post.Recipient != null)
                dbPost.Recipient = post.Recipient.UserId;
            if (post.CreatedAt.HasValue)
                dbPost.Time = post.CreatedAt.Value;
        }

        public static Infrastructure.Model.Post ToBusinessModel(this Post dbPost,int likes=0,int dislikes=0,Model.Profile profile=null,IEnumerable<Model.Comment> comments=null)
        {
            var post = new Model.Post
            {
                Author = new Model.User {UserId = dbPost.Author},
                CreatedAt = dbPost.Time,
                Likes = likes,
                Dislikes = dislikes,
                PostId = dbPost.Pid,
                PostMessage = dbPost.PostMessage,
                PostType = Model.PostType.Post,
                Recipient = new Model.User {UserId = dbPost.Recipient},
                Comments = comments==null?null:comments.Where(c=>c!=null).ToList()
            };
            return post;
        }
    }
}
