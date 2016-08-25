using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ref_val_types
{
    struct PairOfInts
    {
        static int counter = 0;

        public int a;
        public int b;

        internal PairOfInts(int x, int y)
        {
            a = x;
            b = y;
            counter++;
        }
    }

    public struct SomeStruct
    {
        public int a { get; set; }
    }

    class SomeClass
    {
        public SomeStruct str { get; set; }
    }

    class Program : ICloneable
    {
        public PairOfInts pair;
        public string name;

        public Program(PairOfInts p,string s, int x)
        {
            pair = p;
            name = s;
            pair.a += x;
        }

        static void Main(string[] args)
        {
            //--------------------- Memory fun --------------------------
            PairOfInts z = new PairOfInts(1, 2);
            Program p1 = new Program(z, "first", 1);
            Program p2 = new Program(z, "second", 2);
            Program p3 = null;
            Program p4 = p1.Clone() as Program;

            Console.WriteLine(p1.name + " : " + p1.pair.a + " : " + p1.pair.b);
            Console.WriteLine(p4.name + " : " + p4.pair.a + " : " + p4.pair.b);

            p1.name = "hello";
            p1.pair.a = 11;
            p4.pair.b = 12;

            Console.WriteLine(p1.name + " : " + p1.pair.a + " : " + p1.pair.b);
            Console.WriteLine(p4.name + " : " + p4.pair.a + " : " + p4.pair.b);

            //--------------------- Mutable array --------------------------
            //int[] a = { 1, 2, 3, 4 };
            //ChangeArr(a);
            //foreach (var item in a)
            //{
            //    Console.WriteLine(item);
            //}

            Console.ReadLine();
        }

        static void ChangeArr(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] += 2;
            }
        }

        public object Clone()
        { 
            return MemberwiseClone() as Program;
        }
    }
}
