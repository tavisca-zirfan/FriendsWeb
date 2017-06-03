using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Model
{
    public interface IEntity
    {
        object Key { get; }
    }
}
