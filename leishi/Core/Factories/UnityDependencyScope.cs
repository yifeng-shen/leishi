namespace leishi.Web.Core
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    using Microsoft.Practices.Unity;

    internal class UnityDependencyScope : IDependencyScope
    {
        protected readonly IUnityContainer Container;

        internal UnityDependencyScope([Dependency]IUnityContainer container)
        {
            this.Container = container;
        }
        
        public object GetService(Type serviceType)
        {
            if (serviceType != null)
            {
                try
                {
                    return this.Container.Resolve(serviceType);
                }
                catch (ResolutionFailedException)
                {
                }
            }

            return null;
        }
        
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType != null)
            {
                try
                {
                    return this.Container.ResolveAll(serviceType);
                }
                catch (ResolutionFailedException)
                {
                }
            }

            return new List<object>();
        }
        
        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}