using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Data
{
    public interface IReadRepository<T>
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
    }
}
