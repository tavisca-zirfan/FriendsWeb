using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Infrastructure.Data;
using Infrastructure.Model;

namespace DAL
{
    public abstract class EfBaseRepository<T>:IUnitOfWorkRepository,IRepository where T:class 
    {
        IUnitOfWork UnitOfWork { get; set; }
        private IDbSet<T> _entity;

        public IDbSet<T> Entity
        {
            get
            {
                if (_entity == null)
                    _entity = _context.Set<T>();
                return _entity;
            }
        } 
        private DbContext _context;
        public EfBaseRepository()
        {
            _context = UnitOfWork.GetTransactionObject() as DbContext;
        }
        public void PersistAdd(Infrastructure.Model.IEntity entity)
        {
            Entity.Add((T)entity);
        }

        public void PersistDelete(Infrastructure.Model.IEntity entity)
        {
            Entity.Remove((T) entity);
        }

        public void PersistUpdate(Infrastructure.Model.IEntity entity)
        {
            Entity.Attach((T) entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Add(IEntity entity)
        {
            UnitOfWork.RegisterAdd(entity,this);
        }

        public void Update(IEntity entity)
        {
            UnitOfWork.RegisterUpdate(entity,this);
        }

        public void Delete(IEntity entity)
        {
            UnitOfWork.RegisterDelete(entity,this);
        }
    }
}
