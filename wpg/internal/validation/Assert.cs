using System;

namespace Worldpay.@internal.validation
{
    internal class Assert
    {

        public static void notNull(object obj, string message)
        {
            if (obj == null)
            {
                throw new ArgumentException(message);
            }
        }

        public static void notEmpty(string obj, string message)
        {
            if (obj == null || obj.Length == 0)
            {
                throw new ArgumentException(message);
            }
        }

    }
}
