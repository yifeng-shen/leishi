namespace leishi.Web.Core
{
    using System;

    using Microsoft.Practices.Unity;

    public class ModelFactory
    {
        private readonly UnityContainer container;

        public ModelFactory(UnityContainer container)
        {
            this.container = container;
        }

        internal object CreateModel(Type modelType)
        {
            return this.container.Resolve(modelType);
        }
    }
}