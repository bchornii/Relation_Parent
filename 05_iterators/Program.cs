using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_iterators
{   
    delegate void MyDel();
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in GetEnumerator1())
            {
                Console.WriteLine(item);
            }

            //IEnumerator<int> enumerator = GetEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    Console.WriteLine(enumerator.Current);
            //}

            Console.ReadLine();
        }

        static IEnumerable<int> GetEnumerator1()
        {
            foreach (var item in Enumerable.Range(1,10))
            {
                yield return item;
            }
        }

        static IEnumerator<int> GetEnumerator()
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                yield return item;
            }
        }
    }
    
    class A
    {
        public IEnumerable<int> GetEnumerator1()
        {
            foreach (var item in Enumerable.Range(1, 10))
            {
                yield return item;
            }
        }
    }
}
