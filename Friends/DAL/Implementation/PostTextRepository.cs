using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using FriendsDb.Models;

namespace DAL.Implementation
{
    class PostTextRepository:BasePostTypeRepository
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
            if (textPost==null)
                throw new Exception("Wrong object");
            var dbTextPost = new PostText();
            textPost.ToDbModel(dbTextPost);
            Db.PostTexts.Add(dbTextPost);
        }

        public override void RemovePost(FriendsDb.Models.Post post)
        {
            throw new NotImplementedException();
        }
    }
}
