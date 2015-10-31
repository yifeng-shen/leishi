namespace leishi.Web.Core
{
    using System.Web.Http.Dependencies;

    using Microsoft.Practices.Unity;

    internal class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver
    {
        internal UnityDependencyResolver(UnityContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            return new UnityDependencyScope(this.Container.CreateChildContainer());
        }
    }
}