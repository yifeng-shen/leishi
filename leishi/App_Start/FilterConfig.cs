using leishi.Core;
using System.Web.Mvc;

namespace leishi
{
    internal static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorsAttribute());
        }
    }
}
