namespace leishi.Web.Core
{
    using System.ComponentModel;
    using System.Web.Mvc;

    public interface IPropertyBinder
    {
        object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext, MemberDescriptor memberDescriptor);
    }
}