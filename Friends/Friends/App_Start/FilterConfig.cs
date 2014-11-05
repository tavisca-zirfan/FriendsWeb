using System.Reflection.Emit;
using System.Web.Mvc;
using Friends.Classes;

namespace Friends.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}