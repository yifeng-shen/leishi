namespace leishi.Web.Core
{
    using System;
    using System.ComponentModel;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;
    using Common;

    public class DqDefaultModelBinder : DefaultModelBinder
    {
        private readonly ModelFactory modelFactory;

        [InjectionConstructor]
        public DqDefaultModelBinder([Dependency]ModelFactory modelFactory)
            : this()
        {
            this.modelFactory = modelFactory;
        }
        
        public DqDefaultModelBinder()
        {
        }
        
        protected override void BindProperty(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            PropertyDescriptor propertyDescriptor)
        {
            var propertyBinderAttribute = propertyDescriptor.TryFindPropertyBinderAttribute();
            if (propertyBinderAttribute != null)
            {
                var binder = this.CreateBinder(propertyBinderAttribute);
                var value = binder.BindModel(controllerContext, bindingContext, propertyDescriptor);
                propertyDescriptor.SetValue(bindingContext.Model, value);
            }
            else
            {
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
            }
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return modelType == null ? base.CreateModel(controllerContext, bindingContext, null) : this.modelFactory.CreateModel(modelType);
        }
        
        private IPropertyBinder CreateBinder(PropertyBinderAttribute propertyBinderAttribute)
        {
            return (IPropertyBinder)DependencyResolver.Current.GetService(propertyBinderAttribute.BinderType);
        }
    }
}