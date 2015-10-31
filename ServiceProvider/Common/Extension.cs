using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace ServiceProvider.Common
{
    public static class Extension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfArgumentNull(this object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static SecureString ToSecureString(this string value)
        {
            value.ThrowIfArgumentNull("value");
            var secureString = new SecureString();
            if (value.Length > 0)
            {
                foreach (var c in value.ToCharArray())
                {
                    secureString.AppendChar(c);
                }
            }

            return secureString;
        }
    }
}
