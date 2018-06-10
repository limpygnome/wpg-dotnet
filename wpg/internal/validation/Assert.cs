using System;
namespace wpg.@internal.validation
{
    public class Assert
    {

        public static void notNull(object obj, String message)
        {
            if (obj == null)
            {
                throw new ArgumentException(message);
            }
        }

        public static void notEmpty(string obj, String message)
        {
            if (obj == null || obj.Length == 0)
            {
                throw new ArgumentException(message);
            }
        }

    }
}
