using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using FriendsDb.Models;
using Infrastructure.Data;
using Infrastructure.Model;

namespace DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        private Dictionary<object, IUnitOfWorkRepository> _addEntities;
        private Dictionary<object, IUnitOfWorkRepository> _updateEntities;
        private Dictionary<object, IUnitOfWorkRepository> _deleteEntities;
        private DbContext _dbContext;
        public UnitOfWork()
        {
            _dbContext = new FriendsContext();
            _addEntities = new Dictionary<object, IUnitOfWorkRepository>();
            _updateEntities = new Dictionary<object, IUnitOfWorkRepository>();
            _deleteEntities = new Dictionary<object, IUnitOfWorkRepository>();
        }
        public bool Commit()
        {
            foreach (var entity in _addEntities.Keys)
            {
                _addEntities[entity].PersistAdd(entity);
            }
            foreach (var entity in _updateEntities.Keys)
            {
                _updateEntities[entity].PersistAdd(entity);
            }
            foreach (var entity in _deleteEntities.Keys)
            {
                _deleteEntities[entity].PersistAdd(entity);
            }
           
            return _dbContext.SaveChanges()>0;
        }

        public object GetTransactionObject()
        {
            return _dbContext;
        }

        public void RegisterAdd(object entity, IUnitOfWorkRepository repository)
        {
            _addEntities.Add(entity,repository);
        }

        public void RegisterUpdate(object entity, IUnitOfWorkRepository repository)
        {
            _updateEntities.Add(entity,repository);
        }

        public void RegisterDelete(object entity, IUnitOfWorkRepository repository)
        {
            _deleteEntities.Add(entity, repository);
        }
    }
}
