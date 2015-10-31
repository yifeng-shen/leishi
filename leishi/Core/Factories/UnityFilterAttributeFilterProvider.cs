namespace leishi.Web.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;
    
    public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IUnityContainer container;
        
        public UnityFilterAttributeFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
            var controllerAttributes = attributes as FilterAttribute[] ?? attributes.ToArray();
            this.BuildUpAttributes(controllerAttributes);

            return controllerAttributes;
        }
        
        protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
            var filterAttributes = attributes as FilterAttribute[] ?? attributes.ToArray();
            this.BuildUpAttributes(filterAttributes);

            return filterAttributes;
        }
        
        private void BuildUpAttributes(IEnumerable<FilterAttribute> attributes)
        {
            foreach (var attribute in attributes)
            {
                this.container.BuildUp(attribute.GetType(), attribute);
            }
        }
    }
}