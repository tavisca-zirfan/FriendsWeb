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
        void RemovePost(FriendsDb.Models.Post post);
        void SetUnitOfWork(IUnitOfWork uow);
    }
}
