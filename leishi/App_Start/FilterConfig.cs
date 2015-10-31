using leishi.Web.Core;
using System.Web.Mvc;

namespace leishi.Web
{
    internal static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAjaxErrorsAttribute());
        }
    }
}
