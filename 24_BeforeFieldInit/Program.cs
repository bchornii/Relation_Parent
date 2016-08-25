using System;
using System.Threading;

namespace _24_BeforeFieldInit
{
    class Test
    {
        public static string x = EchoAndReturn("In type initializer");
        static Test() { }
        public static string EchoAndReturn(string s)
        {
            Console.WriteLine(s);
            return s;
        }
    }

    class Driver
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Main");
            // Invoke a static method on Test
            Test.EchoAndReturn("Echo!");
            Console.WriteLine("After echo");
            // Reference a static field in Test
            string y = Test.x;
            // Use the value just to avoid compiler cleverness
            if (y != null)
            {
                Console.WriteLine("After field access");
            }

            Console.Read();
        }
    }

    public sealed class Singlton
    {
        public static Singlton Instance { get { return Nested.singlton; } }
        private class Nested
        {
            static Nested() { }
            internal static readonly Singlton singlton = new Singlton();
        }
    }
}
