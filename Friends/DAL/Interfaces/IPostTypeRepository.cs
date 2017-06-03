using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;
using Infrastructure.Data;

namespace DAL.Interfaces
{
    internal interface IPostTypeRepository
    {
        Post ParsePost(FriendsDb.Models.Post post);
        void InsertPost(Post post);
        void RemovePost(Post post);
        void RemovePosts(IEnumerable<Post> posts);
        IQueryable<FriendsDb.Models.Post> IncludeTables(IQueryable<FriendsDb.Models.Post> posts);
        void SetUnitOfWork(IUnitOfWork uow);
    }
}
