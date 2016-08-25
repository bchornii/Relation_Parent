using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_HashCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Some o1 = new Some();
            Some o2 = new Some();

            Console.WriteLine(o1.GetHashCode());
            Console.WriteLine(o2.GetHashCode());

            string s1 = "aaa";
            string s2 = "aaa";

            Console.WriteLine(s1.GetHashCode());
            Console.WriteLine(s2.GetHashCode());

            // key value struct
            Console.WriteLine(new string('-', 80));

            var a = new KeyValuePair<int, string>(10, "aaa");
            var b = new KeyValuePair<int, string>(10, "bbb");

            Console.WriteLine(a.GetHashCode());
            Console.WriteLine(b.GetHashCode());

            Console.WriteLine(new string('-', 80));

            // Для структури перевизначено методи Equals + GetHashCode
            // також реалізовано інтерфейс IEquatable
            Some some = new Some();
            Str str1 = new Str(10, 1212, "hello", some);
            Str str2 = new Str(10, 12, "hello", some);
            
            object oo1 = str1;
            object oo2 = str2;
            Console.WriteLine(str1.Equals(oo1));

            Console.WriteLine(str1.GetHashCode() + " : " + oo1.GetHashCode());
            Console.WriteLine(str2.GetHashCode() + " : " + oo2.GetHashCode());            
            Console.WriteLine(str2.SomeHash());

            Console.WriteLine(new string('-', 80));
            // Різниця в використанні метода екземпляра чи метод статичного в тому що статичний 
            // не кине ексепшин при null строки або строк            
            string ss1 = null;
            string ss2 = null;
            //Console.WriteLine(ss1.Equals(ss2));
            Console.WriteLine(string.Equals(ss1, ss2));

            Console.ReadLine();
        }
    }

    class Some
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    // Якщо не переоприділити GetHashCode то :
    // для структури яка має дані без пробілів в памяті - xor-ряться всі поля структури
    // для структури яка має пробіли або ссилочні типи - вибирається перше поле на основі якого будується 
    //                                                   хеш код, тому треба щоб перше поле було незмінним
    //                                                   бо якщо воно мінятиметься то хеш мінятиметься також і 
    //                                                   структура може загубитись в хеш таблиці
    // для структури в якої перше поле змінне - ламається логіка GetHashCode. CLR xor-рить хеш код даного поля з 
    //                                          вказівником на тип даного поля (MethodTablePointer)
    struct Str //: IEquatable<Str>
    {
        
        public int a;
        public int b;
        public string s;
        public Some obj;

        public Str(int a, int b, string s, Some obj)
        {
            this.a = a;
            this.b = b;
            this.s = s;
            this.obj = obj;
        }

        public int SomeHash()
        {            
            return obj.GetType().GetHashCode() ^ obj.GetHashCode();
        }
        //public bool Equals(Str other)
        //{
        //    return a == other.a && b == other.b &&
        //           s.Equals(other.s);
        //}

        //public override bool Equals(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return false;
        //    }
        //    if (!(obj is Str))
        //    {
        //        return false;
        //    }
        //    return Equals((Str)obj);
        //}

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        return (a ^ b) * 367 + s.GetHashCode();
        //    }
        //}

        //public static bool operator ==(Str str1, Str str2)
        //{
        //    return str1.Equals(str2);
        //}

        //public static bool operator !=(Str str1, Str str2)
        //{
        //    return !(str1 == str2);
        //}
    }
}
