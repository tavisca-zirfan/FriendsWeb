using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using FriendsDb.Models;

namespace DAL.Implementation
{
    internal class PostTextRepository : BasePostTypeRepository
    {
        public override BusinessDomain.DomainObjects.Post ParsePost(FriendsDb.Models.Post post)
        {
            var textPost = post.PostText.ToBusinessModel();
            post.ToBusinessModel(textPost);
            return textPost;
        }

        public override void InsertPost(BusinessDomain.DomainObjects.Post post)
        {
            var textPost = post as TextPost;
            if (textPost == null)
                throw new Exception("Wrong object");
            var dbTextPost = new PostText();
            textPost.ToDbModel(dbTextPost);
            Db.PostTexts.Add(dbTextPost);
        }

        public override void RemovePost(BusinessDomain.DomainObjects.Post post)
        {
            var dbPost = Db.PostTexts.FirstOrDefault(p => p.PostId == post.Id);
            if (dbPost == null)
                return;
            Db.PostTexts.Remove(dbPost);
        }

        public override void RemovePosts(IEnumerable<BusinessDomain.DomainObjects.Post> posts)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<FriendsDb.Models.Post> IncludeTables(IQueryable<FriendsDb.Models.Post> posts)
        {
            return posts.Include(PostType.PostText.ToString());
        }
    }
}
