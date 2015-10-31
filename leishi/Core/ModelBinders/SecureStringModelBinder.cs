namespace leishi.Web.Core
{
    using ServiceProvider.Common;
    using System.Web.Mvc;

    public class SecureStringModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var parameter = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            return (parameter != null ? parameter.AttemptedValue : string.Empty).ToSecureString();
        }
    }
}