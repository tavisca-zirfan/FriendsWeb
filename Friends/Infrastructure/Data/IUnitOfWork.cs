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
        void RegisterAdd(object entity,IUnitOfWorkRepository repository);
        void RegisterUpdate(object entity, IUnitOfWorkRepository repository);
        void RegisterDelete(object entity, IUnitOfWorkRepository repository);
        void Refresh();
    }
}
