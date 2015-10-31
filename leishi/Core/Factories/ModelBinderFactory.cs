namespace leishi.Web.Core
{
    using System;
    using System.Security;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;

    public class ModelBinderFactory : IModelBinderProvider
    {
        private readonly UnityContainer container;
        
        public ModelBinderFactory(UnityContainer container)
        {
            this.container = container;
        }
        
        public IModelBinder GetBinder(Type modelType)
        {
            if (modelType == typeof(SecureString))
            {
                return this.container.Resolve<SecureStringModelBinder>();
            }

            return this.container.Resolve<DqDefaultModelBinder>();
        }
    }
}