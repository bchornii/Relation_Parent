using System;
using System.Collections.Generic;
using System.Linq;

namespace some_mess
{
    class Program
    {
        event EventHandler SomeEvent;
        static void Main(string[] args)
        {
            //string s = getstr();
            //string s2 = string.Copy(s);
            //string s3 = s.Clone() as string;
            //Console.WriteLine("RefEq s and s2 (copy) : " + ReferenceEquals(s, s2));
            //Console.WriteLine("RefEq s and s3 (clone) : " + ReferenceEquals(s, s3));
            //Console.WriteLine("ValEq s and s2 : {0}", s == s2);
            //Console.WriteLine("ValEq s and s3 : {0}", s == s3);
            //Console.WriteLine(sizeof(char));
            //Console.WriteLine(System.Text.Encoding.Unicode.GetByteCount(s));

            //var mc1 = new MyClass();
            //var mc2 = new MyClass();
            //Console.WriteLine(mc1.GetHashCode());
            //Console.WriteLine(mc2.GetHashCode());

            //mc1.a = 34;
            //mc2.a = 35;
            //Console.WriteLine(mc1.GetHashCode());
            //Console.WriteLine(mc2.GetHashCode());

            //Console.WriteLine(new string('-', 80));
            //byte b = 12;
            //int i = 12;

            //Console.WriteLine("byte 12 = int 12 : {0}", b.Equals(i));
            //Console.WriteLine("int 12 = byte 12 : {0}", i.Equals(b));

            //Console.WriteLine(new string('-', 80));
            //HashSet<Foo> set = new HashSet<Foo>();
            //var v1 = new Foo { MyNum = 10, MyStr = "ok", Time = DateTime.Parse("01/01/2016") };
            //var v2 = new Foo { MyNum = 10, MyStr = "ok", Time = DateTime.Parse("01/01/2016") };
            //var v3 = new Foo { MyNum = 10, MyStr = "ok", Time = DateTime.Parse("01/01/2016") };
            //set.Add(v1);
            //set.Add(v2);
            //set.Add(v3);

            ////v1.MyNum = 23;

            //foreach (var item in set)
            //{
            //    Console.WriteLine(item.MyNum + " : " + item.MyStr + " : " + item.Time);
            //    Console.WriteLine(item.GetHashCode());
            //}

            //Console.WriteLine(new string('*', 40));
            //v1.MyNum = 23;

            //foreach (var item in set)
            //{
            //    Console.WriteLine(item.MyNum + " : " + item.MyStr + " : " + item.Time);
            //    Console.WriteLine(item.GetHashCode());
            //}

            //Console.WriteLine(new string('-', 80));

            //int[] arr = { 1, 2, 23, 4, 5, 6 };
            //List<Action> actions_list = new List<Action>();
            //for (int k = 0; k < 5; k++)
            //{               
            //    actions_list.Add(() => Console.WriteLine(k));
            //}

            //for (int k = 0; k < 5; k++)
            //{
            //    actions_list[k]();
            //}

            //Console.WriteLine(new string('-', 80));
            //actions_list = new List<Action>();
            //foreach (var item in Enumerable.Range(1,5))
            //{
            //    actions_list.Add(() => Console.WriteLine(item));
            //}

            //for (int k = 0; k < 5; k++)
            //{
            //    actions_list[k]();
            //}

            // Keyword 'new' bahaviour
            BaseClass bc = new BaseClass();
            DerivedClass dc = new DerivedClass();
            BaseClass bcdc = new DerivedClass();

            // The following two calls do what you would expect. They call
            // the methods that are defined in BaseClass.
            bc.Method1();
            bc.Method2();
            // Output:
            // Base - Method1
            // Base - Method2


            // The following two calls do what you would expect. They call
            // the methods that are defined in DerivedClass.
            dc.Method1();
            dc.Method2();
            // Output:
            // Derived - Method1
            // Derived - Method2


            // The following two calls produce different results, depending 
            // on whether override (Method1) or new (Method2) is used.
            bcdc.Method1();
            bcdc.Method2();
            // Output:
            // Derived - Method1
            // Base - Method2

            Console.Read();
        }

        static string getstr() => "hello";
    }

    class MyClass
    {
        public int a = 10;
        public int b = 20;
    }

    public class Foo : IEquatable<Foo>
    {
        public int MyNum { get; set; }
        public string MyStr { get; set; }
        public DateTime Time { get; set; }

        #region Equality

        public bool Equals(Foo other)
        {
            if (other == null) return false;
            return MyNum == other.MyNum &&
                Time == other.Time &&
                string.Equals(MyStr, other.MyStr);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Foo);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ MyNum;
                var myStrHashCode =
                    !string.IsNullOrEmpty(MyStr) ?
                        MyStr.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ myStrHashCode;
                hashCode =
                    (hashCode * 397) ^ Time.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }

    class BaseClass
    {
        public virtual void Method1()
        {
            Console.WriteLine("Base - Method1");
        }

        public virtual void Method2()
        {
            Console.WriteLine("Base - Method2");
        }
    }

    class DerivedClass : BaseClass
    {
        public override void Method1()
        {
            Console.WriteLine("Derived - Method1");
        }

        public new void Method2()
        {
            Console.WriteLine("Derived - Method2");
        }
    }

}
