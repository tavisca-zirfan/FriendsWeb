using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using FriendsDb.Models;
using Infrastructure.Container;
using Infrastructure.Data;
using Infrastructure.Model;
using Model=BusinessDomain.DomainObjects;

namespace DAL
{
    public class PostRepository:IPostRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }
        private FriendsContext Db;
        private PostResponseRepository _postResponseRepository;
        public PostRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            if(unitOfWork==null)
                Db = new FriendsContext();
            else
            {
                Db = UnitOfWork.GetTransactionObject() as FriendsContext;
            }
            _postResponseRepository = new PostResponseRepository(unitOfWork);
        }

        public PostRepository():this(null)
        {
            
        }

        private IPostTypeRepository GetRepository(Model.PostType postType)
        {
            var repo = ObjectFactory.Resolve<IPostTypeRepository>(postType.ToString());
            repo.SetUnitOfWork(UnitOfWork);
            return repo;
        }

        public void AddPost(Model.Post post)
        {
            var dbPost = new Post();
            post.ToBaseDbModel(dbPost);
            Db.Posts.Add(dbPost);
            var repo = GetRepository(post.PostType);
            repo.InsertPost(post);
        }

        public string DeletePost(Model.Post post)
        {
            var dbPost =
                Db.Posts.FirstOrDefault(p => p.Pid == post.Id);
            if (dbPost == null)
                return null;

            //Db.Posts.Remove(dbPost);
            _postResponseRepository.DeleteComment(dbPost.Pid, post.PostType);
            _postResponseRepository.RemoveLike(new List<string>{dbPost.Pid},Model.PostType.PostText );
            return dbPost.Pid;
        }

        public void UpdatePost(Model.Post post)
        {
            throw new NotImplementedException();
        }

        //public Model.Post GetPost(string postId, Model.PostType postType, string userId)
        //{
        //    var post = (from c in Db.Comments
        //                where c.Type == Model.PostType.Post.ToString()
        //                let cl = Db.Likes.Where(l => l.TypeId == c.CommentId && l.Type == Model.PostType.Comment.ToString())
        //                group new { Comment = c, Likes = cl.Count(l => l.LikeType == (int)Model.LikeType.Like), Dislikes = cl.Count(l => l.LikeType == (int)Model.LikeType.Dislike) } by c.TypeId
        //                    into com
        //                    join p in Db.Posts on com.Key equals p.Pid
        //                    where p.Pid == postId
        //                    let pl = Db.Likes.Where(l => l.TypeId == p.Pid && l.Type == Model.PostType.Post.ToString())
        //                    select new
        //                    {
        //                        Post = p,
        //                        Comments = com.Select(c => c),
        //                        Likes = pl.Count(l => l.LikeType == (int)Model.LikeType.Like),
        //                        Dislikes = pl.Count(l => l.LikeType == (int)Model.LikeType.Dislike),
        //                    }).FirstOrDefault();
        //    if (post == null)
        //        return null;
        //    return post.Post.ToBusinessModel(post.Likes, post.Dislikes, null,
        //        post.Comments.Select(c => c.Comment.ToBusinessModel(c.Likes, c.Dislikes)).ToList());
        //}

        public Model.Post GetPost(string postId, Model.PostType postType)
        {
            var tables = new List<string> {postType.ToString()};
            var dbPost =
                GetPosts(tables)
                    .FirstOrDefault(p=>p.Pid==postId);
            var postTypeRepo = ObjectFactory.Resolve<IPostTypeRepository>(postType.ToString());
            var comments = GetComments(new List<string> {dbPost.Pid});
            var post= postTypeRepo.ParsePost(dbPost);
            dbPost.ToBusinessModel(post);
            post.Comments = comments.ToList();
            return post;
        }

        private IEnumerable<Model.Comment> GetComments(IEnumerable<string> postIds)
        {
            var tables = new List<string> { Model.PostType.Comment.ToString() };
            var dbComments = GetPosts(tables)
                .Where(p => postIds.Contains(p.Comment.ForPostId)).ToList();
            var commentRepo = ObjectFactory.Resolve<IPostTypeRepository>(Model.PostType.Comment.ToString());
            return dbComments.Select(commentRepo.ParsePost).Cast<Model.Comment>();
        }

        private IQueryable<Post> GetPosts(IEnumerable<string> tables)
        {
            var posts = Db.Posts
                .Include("UserProfile")
                    .Include("PostRecipients");
            posts = tables.Aggregate(posts, (current, table) => current.Include(table));
            return posts;
        }

        public IEnumerable<Model.Post> GetPosts(SearchFilter filter)
        {
            //var post = (from p in Db.Posts
            //            from c in Db.Comments
            //                .Where(
            //                    com =>
            //                        com.TypeId == p.Pid && com.Type == Model.PostType.Post.ToString()  &&
            //                        (p.Author == filter.FilterProperties["authorid"].ToString() || p.Recipient == filter.FilterProperties["recipientid"].ToString())).DefaultIfEmpty()
            //            group c by p
            //                into com
            //                select new
            //                {
            //                    Likes =
            //                        Db.Likes.Count(
            //                            l =>
            //                                l.TypeId == com.Key.Pid && l.Type == Model.PostType.Post.ToString() &&
            //                                l.LikeType == (int)Model.LikeType.Like),
            //                    Dislikes =
            //                        Db.Likes.Count(
            //                            l =>
            //                                l.TypeId == com.Key.Pid && l.Type == Model.PostType.Post.ToString() &&
            //                                l.LikeType == (int)Model.LikeType.Dislike),
            //                    Post = com.Key,
            //                    Comments = com.Select(c => new
            //                    {
            //                        Comment = c,
            //                        like = Db.Likes.Count(
            //                            l =>
            //                                l.TypeId == c.CommentId && l.Type == Model.PostType.Comment.ToString() &&
            //                                l.LikeType == (int)Model.LikeType.Like)
            //                        ,
            //                        dislike = Db.Likes.Count(
            //                            l =>
            //                                l.TypeId == c.CommentId && l.Type == Model.PostType.Comment.ToString() &&
            //                                l.LikeType == (int)Model.LikeType.Dislike)
            //                    })


            //                })
            ////var p = Db.Posts.FirstOrDefault();p.
            ////var post = Db.Posts.Include("Comments").Include("")
            //return post.Select(p=>p.Post.ToBusinessModel(p.Likes, p.Dislikes, null,
            //    p.Comments.Select(c =>c.Comment==null?null:c.Comment.ToBusinessModel(c.like,c.dislike)).ToList()));
            throw new NotImplementedException();
        }

    }
}
