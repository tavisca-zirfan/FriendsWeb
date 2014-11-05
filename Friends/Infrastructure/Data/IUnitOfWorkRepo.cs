using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace Infrastructure.Data
{
    public interface IUnitOfWorkRepository
    {
        void PersistAdd(IEntity entity);
        void PersistDelete(IEntity entity);
        void PersistUpdate(IEntity entity);
    }
}
