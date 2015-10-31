namespace leishi.Web.Core
{
    using System;

    public class PropertyBinderAttribute : Attribute
    {
        public PropertyBinderAttribute(Type binderType)
            : this(binderType, string.Empty)
        {
        }

        public PropertyBinderAttribute(Type binderType, string propertyKey)
        {
            this.PropertyKey = propertyKey;
            this.BinderType = binderType;
        }

        public Type BinderType { get; private set; }

        public string PropertyKey { get; private set; }
    }
}