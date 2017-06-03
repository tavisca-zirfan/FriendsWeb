using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessDomain.DomainObjects;

namespace BusinessDomain.Interface
{
    public interface ILikable
    {
        void Like(User userId);
        void Dislike(User userId);
    }
}
