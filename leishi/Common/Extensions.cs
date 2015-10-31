namespace leishi.Web.Common
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using System.Xml.Linq;
    using Core;

    public static class Extensions
    {
        public static bool GetRequestValue(this IValueProvider valueProvider, string key, out string value)
        {
            value = string.Empty;
            var valueProviderResult = valueProvider.GetValue(key);
            if (valueProviderResult == null)
            {
                return false;
            }

            value = valueProviderResult.AttemptedValue;
            return !string.IsNullOrEmpty(value);
        }

        public static string GetAttributeOrDefault(this XElement element, string attributeName, string defaultValue = "")
        {
            var attr = element.Attribute(attributeName);
            return attr != null ? attr.Value : defaultValue;
        }

        public static SelectList ToSelectListWithDescription<TEnum>(this TEnum enumObject)
            where TEnum : struct
        {
            var values =
                Enum.GetValues(typeof(TEnum))
                    .Cast<TEnum>()
                    .Select<TEnum, object>(
                        e =>
                            {
                                var field = e.GetType().GetField(e.ToString());
                                var attributes =
                                    (DescriptionAttribute[])
                                    field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                                return
                                    new
                                        {
                                            Value = Convert.ChangeType(e, typeof(int), CultureInfo.InvariantCulture),
                                            Text = attributes.Length > 0 ? attributes[0].Description : e.ToString()
                                        };
                            });

            return new SelectList(values, "Value", "Text", enumObject);
        }
        
        public static PropertyBinderAttribute TryFindPropertyBinderAttribute(this MemberDescriptor propertyDescriptor)
        {
            return propertyDescriptor.Attributes
              .OfType<PropertyBinderAttribute>()
              .FirstOrDefault();
        }
    }
}