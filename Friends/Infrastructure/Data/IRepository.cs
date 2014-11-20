using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace Infrastructure.Data
{
    public interface IRepository
    {
        void Add(object entity);
        void Update(object entity);
        void Delete(object entity);
    }
}
