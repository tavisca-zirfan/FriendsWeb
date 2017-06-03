using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Infrastructure.Data;
using Infrastructure.Model;

namespace DAL
{
    public partial class EfBaseRepository<T>:IUnitOfWorkRepository,IRepository,IReadRepository<T> where T:class 
    {
        IUnitOfWork UnitOfWork { get; set; }
        private IDbSet<T> _entity;

        public IDbSet<T> Entity
        {
            get
            {
                if (_entity == null)
                    _entity = Context.Set<T>();
                return _entity;
            }
        } 
        protected DbContext Context;

        public EfBaseRepository()
        {
            UnitOfWork = new UnitOfWork();
        }
        public EfBaseRepository(IUnitOfWork uow)
        {
            UnitOfWork = uow;
            Context = UnitOfWork.GetTransactionObject() as DbContext;
        }
        public void PersistAdd(Object entity)
        {
            Entity.Add((T)entity);
        }

        public void PersistDelete(Object entity)
        {
            Entity.Remove((T) entity);
        }

        public void PersistUpdate(Object entity)
        {
            
        }

        public void Add(object entity)
        {
            UnitOfWork.RegisterAdd(entity,this);
        }

        public void Update(object entity)
        {
            UnitOfWork.RegisterUpdate(entity,this);
        }

        public void Delete(object entity)
        {
            UnitOfWork.RegisterDelete(entity,this);
        }

        public T GetById(object id)
        {
            return Entity.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Entity;
        }
    }
}
