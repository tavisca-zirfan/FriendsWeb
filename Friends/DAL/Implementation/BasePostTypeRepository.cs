using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using FriendsDb.Models;
using Infrastructure.Data;
using Post = BusinessDomain.DomainObjects.Post;

namespace DAL.Implementation
{
    internal abstract class BasePostTypeRepository:IPostTypeRepository
    {
        protected FriendsContext Db;
        protected BasePostTypeRepository(IUnitOfWork uow=null)
        {
            if (uow != null)
                Db = uow.GetTransactionObject() as FriendsContext;
            
        }

        public abstract Post ParsePost(FriendsDb.Models.Post post);


        public abstract void InsertPost(Post post);


        public abstract void RemovePost(FriendsDb.Models.Post post);



        public void SetUnitOfWork(IUnitOfWork uow)
        {
            Db = uow.GetTransactionObject() as FriendsContext;
        }
    }
}
