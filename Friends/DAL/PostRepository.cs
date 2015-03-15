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
            if (post.PostType != Model.PostType.Comment && post.Comments.Count>0)
            {
                var commentRepo = GetRepository(Model.PostType.Comment);
                commentRepo.RemovePosts(post.Comments);
                var commentsToBeRemoved = post.Comments.Select(c => c.Id).ToList();
                Db.Posts.RemoveRange(Db.Posts.Where(p => commentsToBeRemoved.Contains(p.Pid)));
            }
            var childRepository = GetRepository(post.PostType);
            childRepository.RemovePost(post);
            Db.PostTags.RemoveRange(Db.PostTags.Where(t => t.PostId == post.Id));
            Db.PostRecipients.RemoveRange(Db.PostRecipients.Where(r => r.PostId == post.Id));
            Db.Likes.RemoveRange(Db.Likes.Where(l => l.PostId == post.Id));
            Db.Posts.Remove(dbPost);
            return dbPost.Pid;
        }

        public void UpdatePost(Model.Post post)
        {
            throw new NotImplementedException();
        }

        public Model.Post GetPost(string postId, Model.PostType postType)
        {
            var tables = new List<string> {postType.ToString()};
            var dbPost =
                GetPosts(tables)
                    .FirstOrDefault(p=>p.Pid==postId);
            if (dbPost == null)
                return null;
            var postTypeRepo = ObjectFactory.Resolve<IPostTypeRepository>(postType.ToString());
            var comments = GetComments(new List<string> {dbPost.Pid});
            var post= postTypeRepo.ParsePost(dbPost);
            post.Comments = comments.ToList();
            return post;
        }

        private IEnumerable<Model.Comment> GetComments(IEnumerable<string> postIds)
        {
            var tables = new List<string> { Model.PostType.Comment.ToString() };
            var dbComments = GetPosts(tables)
                .Where(p => postIds.Contains(p.Comment.ForPostId)).ToList();
            var commentRepo = ObjectFactory.Resolve<IPostTypeRepository>(Model.PostType.Comment.ToString());
            return dbComments.Select(commentRepo.ParsePost).Cast<Model.Comment>().ToList();
        }

        private IQueryable<Post> GetPosts(IEnumerable<string> types)
        {
            IQueryable<Post> posts = Db.Posts
                .Include("UserProfile")
                    .Include("PostRecipients").Include("PostTags").Include("Likes");
            return types.Select(ObjectFactory.Resolve<IPostTypeRepository>)
                .Aggregate(posts, (current, childRepository) => childRepository.IncludeTables(current));
        }

        public IEnumerable<Model.Post> GetPosts(SearchFilter filter,IEnumerable<Model.PostType> types)
        {
            var postsTable = GetPosts(types.Select(t=>t.ToString()));
            var posts =
                postsTable.Where(
                    p =>
                        p.Author == filter.FilterProperties["authorid"].ToString() ||
                        p.PostRecipients.Select(r => r.RecipientId).Contains(filter.FilterProperties["recipientid"].ToString()))
                    .ToList();
            var comments = GetComments(posts.Select(p => p.Pid));
            var finalList = posts.Select(p =>
            {
                var childRepository = ObjectFactory.Resolve<IPostTypeRepository>(p.Type);
                var post= p.ToBusinessModel(childRepository.ParsePost(p));
                post.Comments = comments.Where(c => c.ForPostId == p.Pid) as IList<BusinessDomain.DomainObjects.Comment>;
                return post;
            }).ToList();
            return finalList;
        }



        public void AddLike(string postId, string userId, Model.LikeType likeType)
        {
            Db.Likes.Add(new Like {PostId = postId, LikeType = (int) likeType, Time = DateTime.Now, UserId = userId});
        }

        public void RemoveLike(string postId, string userId)
        {
            var like = Db.Likes.Where(l => l.PostId == postId && l.UserId== userId);
            Db.Likes.RemoveRange(like);
        }

        public void AddRecipient(string postId, string userId)
        {
            Db.PostRecipients.Add(new PostRecipient {PostId = postId, RecipientId = userId});
        }

        public void RemoveRecipient(string postId, string userId)
        {
            var recipient = Db.PostRecipients.First(r => r.PostId == postId && r.RecipientId == userId);
            if (recipient == null)
                return;
            Db.PostRecipients.Remove(recipient);
        }

        public void AddTag(string postId, string userId)
        {
            Db.PostTags.Add(new PostTag { PostId = postId, UserId = userId });
        }

        public void RemoveTag(string postId, string userId)
        {
            var tag = Db.PostTags.First(l => l.PostId == postId && l.UserId == userId);
            if (tag == null)
                return;
            Db.PostTags.Remove(tag);
        }
    }
}
