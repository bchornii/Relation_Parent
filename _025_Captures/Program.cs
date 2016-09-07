using System;
using System.Collections.Generic;
using System.Linq;

namespace _025_Captures
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8 };

            foreach (var item in GetIntegersWithClass(arr))
            {
                Console.WriteLine(item);
            }

            Console.Read();
        }

        static IEnumerable<int> GetIntegers(IEnumerable<int> source, int max)
        {
            return source.Where(i => i < max);
        }

        static IEnumerable<int> GetIntegers(IEnumerable<int> source)
        {
            return source.Where(i => i < 10);
        }

        static IEnumerable<int> GetIntegersWithClass(IEnumerable<int> source)
        {
            using(var cls = GetClass())
            {
                return source.Where(i => i < cls.a);
            }            
        }

        static MyClass GetClass()
        {
            return new MyClass();
        }
    }

    class MyClass : IDisposable
    {
        public int a => 10;
        public void Dispose()
        {            
        }
    }
}
