using System.Web;
using System.Web.Mvc;
using AspNet.Filters;

namespace AspNet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
