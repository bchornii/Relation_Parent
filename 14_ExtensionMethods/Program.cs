using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_ExtensionMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = null;
        }
    }

    static class Extension
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrEmpty(this string text, bool checkForNull)
        {
            if (checkForNull)
            {
                if (text == null)
                {
                    throw new ArgumentNullException();
                }
            }
            return string.IsNullOrEmpty(text);
        }
    }
}
