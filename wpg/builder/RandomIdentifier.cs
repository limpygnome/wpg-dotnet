using System;
using System.Text;

namespace Worldpay
{
    public class RandomIdentifier
    {
        private static readonly Random random = new Random();

        public static String generate(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                // generate char 0-9 or a-z
                int r = random.Next(36);
                char c = (char)((r < 10 ? '0' : 'W') + r);
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
