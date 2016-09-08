using System;
using System.Collections.Generic;

namespace some_mess
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = getstr();
            string s2 = string.Copy(s);
            string s3 = s.Clone() as string;
            Console.WriteLine("RefEq s and s2 (copy) : " + ReferenceEquals(s, s2));
            Console.WriteLine("RefEq s and s3 (clone) : " + ReferenceEquals(s, s3));
            Console.WriteLine("ValEq s and s2 : {0}", s == s2);
            Console.WriteLine("ValEq s and s3 : {0}", s == s3);
            Console.WriteLine(sizeof(char));
            Console.WriteLine(System.Text.Encoding.Unicode.GetByteCount(s));

            var mc1 = new MyClass();
            var mc2 = new MyClass();
            Console.WriteLine(mc1.GetHashCode());
            Console.WriteLine(mc2.GetHashCode());

            mc1.a = 34;
            mc2.a = 35;
            Console.WriteLine(mc1.GetHashCode());
            Console.WriteLine(mc2.GetHashCode());

            Console.WriteLine(new string('-', 80));
            byte b = 12;
            int i = 12;

            Console.WriteLine("byte 12 = int 12 : {0}", b.Equals(i));
            Console.WriteLine("int 12 = byte 12 : {0}", i.Equals(b));

            Console.WriteLine(new string('-', 80));
            List<Foo> set = new List<Foo>();
            var v1 = new Foo { MyNum = 10, MyStr = "ok", Time = DateTime.Parse("01/01/2016") };
            var v2 = new Foo { MyNum = 10, MyStr = "ok", Time = DateTime.Parse("01/01/2016") };
            var v3 = new Foo { MyNum = 10, MyStr = "ok", Time = DateTime.Parse("01/01/2016") };
            set.Add(v1);
            set.Add(v2);
            set.Add(v3);

            //v1.MyNum = 23;

            foreach (var item in set)
            {
                Console.WriteLine(item.MyNum + " : " + item.MyStr + " : " + item.Time);
                Console.WriteLine(item.GetHashCode());
            }

            v1.MyNum = 23;

            foreach (var item in set)
            {
                Console.WriteLine(item.MyNum + " : " + item.MyStr + " : " + item.Time);
                Console.WriteLine(item.GetHashCode());
            }

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

}
