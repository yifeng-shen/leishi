using System;
using System.Web;
using System.Web.Mvc;

namespace leishi.Web.Core
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class GlobalAuthorizeAttribute : AuthorizeAttribute
    {
        public GlobalAuthorizeAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            // TODO
            return base.AuthorizeCore(httpContext);
        }
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            // TODO
        }
    }
}
