using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Common
{
    public static class IdGenerator
    {
        private static RainDrop _rainDrop;
        static IdGenerator()
        {
            _rainDrop=new RainDrop();
        }
        public static string GenerateId()
        {
            return _rainDrop.GetNextId();
        }
    }
}
