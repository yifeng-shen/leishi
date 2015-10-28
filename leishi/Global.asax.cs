﻿using leishi.Common;
using leishi.Controllers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace leishi
{
    public class MvcApplication : HttpApplication
    {
        private UnityContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // Register Handler before register route table.
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.ConfigureIocContainer(GlobalConfiguration.Configuration);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = new HttpContextWrapper(((MvcApplication)sender).Context);
            var ex = Server.GetLastError();

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.ContentType = "text/html";
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            var errorInfo = new HandleErrorInfo(ex, HttpContextUtility.CurrentController, HttpContextUtility.CurrentAction);

            //var message = Logger.BuildMessage(
            //   "Web Application Error Controller:{0}, Action:{1}. Message: {2}, InnerException: {3}, StackTrace: {4}",
            //   errorInfo.ControllerName,
            //   errorInfo.ActionName,
            //       errorInfo.Exception.Message,
            //       errorInfo.Exception.InnerException,
            //       errorInfo.Exception.StackTrace);
            //Logger.Instance.LogError(Dqms.Common.Logging.Events.WebApplicationError, message);

            // Format and add log here.

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            routeData.Values["errorInfo"] = errorInfo;
            
            ((IController)new ErrorController()).Execute(new RequestContext(httpContext, routeData));
        }

        private void ConfigureIocContainer(HttpConfiguration config)
        {
            this.container = new UnityContainer();

            this.container.RegisterInstance(new ServiceFactory(this.container), new ContainerControlledLifetimeManager());
            this.container.RegisterInstance(new ModelFactory(this.container), new ContainerControlledLifetimeManager());

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            this.container.LoadConfiguration(section, "UnityContainer");

            this.container.RegisterType<IMetadataUtility, CachedMetadataUtility>(new TransientLifetimeManager(), new InjectionConstructor());

            this.container.RegisterInstance(this.container.Resolve<CheckModelClient>(), new ContainerControlledLifetimeManager());

            ModelBinderProviders.BinderProviders.Clear();
            ModelBinderProviders.BinderProviders.Add(new ModelBinderFactory(this.container));
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(this.container));
            config.DependencyResolver = new UnityDependencyResolver(this.container);

            var filterProvider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProvider);

            var unityFilterAttributeFilterProvider = new UnityFilterAttributeFilterProvider(this.container);
            FilterProviders.Providers.Add(unityFilterAttributeFilterProvider);
        }
    }
}
