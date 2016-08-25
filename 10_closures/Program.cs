using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Closure works by catching variable not value

namespace _09_closures
{
    delegate Base CovariantDel();
    delegate void ContravarianceDel(Derived parameter);
    delegate Base CoContraDel(Derived parameter);

    class Program
    {        
        static void Main(string[] args)
        {
            //Action[] instances = new Action[10];

            //int x;
            //int z;
            //for (int i = 0; i < instances.Length; i++)
            //{
            //    x = 0;
            //    z = 0;
            //    int y = 0;
            //    instances[i] = () =>
            //    {
            //        Console.WriteLine("x ={0}, y={1}", x, y, z);
            //        x++;
            //        y++;
            //        z++;                    
            //    };
            //}

            //instances[0]();
            //instances[0]();
            //instances[0]();
            //instances[1]();
            //instances[0]();
            //x = 10;
            //instances[2]();

            VarClass vc = new VarClass { MaxLen = 0 };
            string[] words = { "hello", "my", "sweet", "world", "!!!!!!!" };

            // In C# the variable itself has been captured by the delegate
            //-----------------------------------------------------------------------------------------
            // Changes closured var from 3 to 5 -> affected in predicate
            //Console.WriteLine("Now for words with <= 3 letters:");
            //vc.MaxLen = 3;

            //Predicate<string> predicate = item => item.Length <= vc.MaxLen;
            //IEnumerable<string> shortWords = ListUtil.Filter(words, predicate);
            //ListUtil.Dump(shortWords);

            //Console.WriteLine("Now for words with <= 5 letters:");
            //vc.MaxLen = 5;
            //shortWords = ListUtil.Filter(words, predicate);
            //ListUtil.Dump(shortWords);

            //-----------------------------------------------------------------------------------------
            // Changes var in closure -> affected local variable
            Console.WriteLine("Start value = {0}", vc.MaxLen);
            Predicate<string> predicate = item => { vc.MaxLen++; return item.Length <= vc.MaxLen; };
            IEnumerable<string> shortWords = ListUtil.Filter(words, predicate);
            ListUtil.Dump(shortWords);
            Console.WriteLine("Start value = {0}", vc.MaxLen);

            Console.ReadLine();
        }

        static Derived GetSomeValue()
        {
            return new Derived();
        }
        static void SetSomeValue(Base parameter)
        {

        }
        static Derived GetSetSomeValue(Base parameter)
        {
            return new Derived();
        }
    }

    public static class ListUtil
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            var list = new List<T>();
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static void Dump(IEnumerable<string> source)
        {            
            foreach (var item in source)
            {
                Console.WriteLine(item);
            }            
        }
    }

    class Base
    {

    }
    class Derived : Base
    {

    }

    class VarClass
    {
        public int MaxLen { get; set; }
    }

}
