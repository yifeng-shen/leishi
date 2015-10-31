using System.Net;
using System.Web.Mvc;

namespace leishi.Web.Core
{
    public class HandleErrorsAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 特殊处理异步请求异常
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                base.OnException(filterContext);
            }

            var resetSession = filterContext.Exception is HttpAntiForgeryException;

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = resetSession
                                                                ? (int)HttpStatusCode.RequestTimeout
                                                                : (int)HttpStatusCode.InternalServerError;

            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data =
                                               new
                                               {
                                                   message =
                                               resetSession
                                                   ? Resources.Common.AjaxRequestResetMessage
                                                   : Resources.Common.InternalServerErrorText
                                               }
            };
        }
    }
}
