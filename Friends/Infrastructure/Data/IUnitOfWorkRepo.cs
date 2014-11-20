using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace Infrastructure.Data
{
    public interface IUnitOfWorkRepository
    {
        void PersistAdd(Object entity);
        void PersistDelete(Object entity);
        void PersistUpdate(Object entity);
    }
}
