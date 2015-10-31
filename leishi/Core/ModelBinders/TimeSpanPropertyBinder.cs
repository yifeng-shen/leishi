namespace leishi.Web.Core
{
    using Common;
    using System;
    using System.ComponentModel;
    using System.Web.Mvc;

    public class TimeSpanPropertyBinder : IPropertyBinder
    {
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            MemberDescriptor memberDescriptor)
        {
            var propertyAttribute = memberDescriptor.TryFindPropertyBinderAttribute();
            if (propertyAttribute == null || string.IsNullOrEmpty(propertyAttribute.PropertyKey))
            {
                return null;
            }

            var propertyKey = propertyAttribute.PropertyKey;
            string propertyValue;
            TimeSpan timeSpan;
            if (!bindingContext.ValueProvider.GetRequestValue(propertyKey, out propertyValue) ||
                !TimeSpan.TryParse(propertyValue, out timeSpan))
            {
                return new TimeSpan();
            }

            return timeSpan;
        }
    }
}