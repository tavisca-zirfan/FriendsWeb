using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using FriendsDb.Models;
using Infrastructure.Model;

namespace DAL.Implementation
{
    public class PostListUserFilter:BaseFilterParser<Post>
    {
        
        public override IQueryable<FriendsDb.Models.Post> CreateFilter(IQueryable<Post> list,SearchFilter filter)
        {
            if (FilterParser != null)
                list = FilterParser.CreateFilter(list, filter);
            var user = filter.Get("userid");
            if (user != null)
                list = list.Where(
                    p =>
                        p.Author == user.ToString() ||
                        p.PostRecipients.Select(r => r.RecipientId).Contains(user.ToString()));
            
            return list;
        }
    }

    public class PostListBaseFilter : BaseFilterParser<Post>
    {
        
        public override IQueryable<FriendsDb.Models.Post> CreateFilter(IQueryable<Post> list, SearchFilter filter)
        {
            if (FilterParser != null)
                list = FilterParser.CreateFilter(list, filter);
            var types = filter.Get("types").ToString().Split(',');
            if (types.Any())
                list = list.Where(
                    p =>
                        types.Contains(p.Type));
            return list;
        }


        
    }

    public class PostListLastUpdateFilter : BaseFilterParser<Post>
    {
       
        public override IQueryable<FriendsDb.Models.Post> CreateFilter(IQueryable<Post> list, SearchFilter filter)
        {
            if (FilterParser != null)
                list = FilterParser.CreateFilter(list, filter);
            var lastUpdate = filter.Get("lastupdate");
            if (lastUpdate != null)
            {
                DateTime dtLastUpdate;
                if (DateTime.TryParse(lastUpdate.ToString(), out dtLastUpdate))
                    list = list.Where(p => p.LastUpdate > dtLastUpdate);
            }
            return list;
        }
    }

}
