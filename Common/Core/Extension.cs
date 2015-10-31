using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace leishi.Common
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

        public static string ToUnsecureString(this SecureString secureString)
        {
            var stringPtr = IntPtr.Zero;
            try
            {
                stringPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(stringPtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(stringPtr);
            }
        }

        public static string ToStringSafe(this object obj)
        {
            return obj == null
                       ? string.Empty
                       : obj.ToString();
        }
    }
}
