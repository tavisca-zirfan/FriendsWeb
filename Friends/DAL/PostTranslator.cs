using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using FriendsDb.Models;
using Model = BusinessDomain.DomainObjects;
using Post = FriendsDb.Models.Post;

namespace DAL
{
    public static class PostTranslator
    {
        public static void ToBaseDbModel(this Model.Post post, Post dbPost)
        {
            dbPost.Pid = post.Id;
            dbPost.Type = post.PostType.ToString();
            if (post.Author != null)
                dbPost.Author = post.Author.Id;
            if (post.Recipients != null && post.Recipients.Count > 0)
            {
                foreach (var recipient in post.Recipients)
                {
                    dbPost.PostRecipients.Add(new PostRecipient{PostId = dbPost.Pid,RecipientId = recipient.Id});
                }
            }
            if (post.Tags != null && post.Tags.Count > 0)
            {
                foreach (var recipient in post.Tags)
                {
                    dbPost.PostTags.Add(new PostTag { PostId = dbPost.Pid, UserId = recipient.Id });
                }
            }
            if (post.CreatedAt.HasValue)
                dbPost.Time = post.CreatedAt.Value;
            dbPost.LastUpdate = post.LastUpdate;
        }

        public static BusinessDomain.DomainObjects.Post ToBusinessModel(this Post dbPost, Model.Post post, int likes = 0,
            int dislikes = 0)
        {
            
            post.Author = dbPost.UserProfile.ToBusinessModel();
            post.CreatedAt = dbPost.Time;
            post.Likes = dbPost.Likes.Where(l=>l.LikeType==(int)Model.LikeType.Like).Select(l=>l.UserProfile.ToBusinessModel()).ToList();
            post.Dislikes = dbPost.Likes.Where(l => l.LikeType == (int)Model.LikeType.Dislike).Select(l => l.UserProfile.ToBusinessModel()).ToList(); ;
            post.Id = dbPost.Pid;
            post.Recipients = dbPost.PostRecipients.Select(p => p.UserProfile.ToBusinessModel()).ToList();
            post.Tags = dbPost.PostTags.Select(p => p.UserProfile.ToBusinessModel()).ToList();
            return post;
        }
    }

    public static class TextPostTranslator
    {
        public static void ToDbModel(this Model.TextPost post, PostText dbPost)
        {
            dbPost.PostId = post.Id;
            dbPost.Text = post.Message;
        }

        public static Model.TextPost ToBusinessModel(this PostText dbPost, Model.TextPost textPost=null)
        {
            if(textPost==null)
                textPost=new Model.TextPost();
            textPost.Message = dbPost.Text;
            return textPost;
        }
    }

    public static class CommentTranslator
    {
        public static void ToDbModel(this Model.Comment post, Comment dbComment)
        {
            dbComment.CommentId = post.Id;
            dbComment.CommentMessage = post.CommentMessage;
            dbComment.ForPostId = post.ForPostId;
        }

        
    }
}
