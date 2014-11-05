using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace Infrastructure.Data
{
    public interface IUnitOfWork
    {
        bool Commit();
        object GetTransactionObject();
        void RegisterAdd(IEntity entity,IUnitOfWorkRepository repository);
        void RegisterUpdate(IEntity entity, IUnitOfWorkRepository repository);
        void RegisterDelete(IEntity entity, IUnitOfWorkRepository repository);
    }
}
