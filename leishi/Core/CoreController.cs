using leishi.Common;
using System.Web.Mvc;

namespace leishi.Web.Core
{
    public class CoreController : Controller
    {
        protected readonly log4net.ILog logger;

        public CoreController()
        {
            this.logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // if the request is AJAX return.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            ViewBag.LoginMenu = filterContext.HttpContext.Request.QueryString["u"].ToStringSafe().ToLower() == Resources.Common.AdminLoginQueryStringKey;

            base.OnActionExecuting(filterContext);
        }
    }
}
