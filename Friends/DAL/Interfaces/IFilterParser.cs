using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model;

namespace DAL.Interfaces
{
    public interface IFilterParser<T>
    {
        IQueryable<T> CreateFilter(IQueryable<T> list,SearchFilter filter);
        void SetBaseFilter(IFilterParser<T> baseFilterParser);
    }

    public abstract class BaseFilterParser<T> : IFilterParser<T>
    {
        public IFilterParser<T> FilterParser { get; set; } 
        
        public abstract IQueryable<T> CreateFilter(IQueryable<T> list, SearchFilter filter);



        public void SetBaseFilter(IFilterParser<T> baseFilterParser)
        {
            FilterParser = baseFilterParser;
        }
    }
}
