using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22_strings_in_net
{
    class Program
    {
        static void Main(string[] args)
        {
            //long b1 = GC.GetTotalMemory(true);
            //string s = "hello";
            //long b2 = GC.GetTotalMemory(true);

            //Console.WriteLine("String len = {0}", s.Length);
            //Console.WriteLine("Bytes len = {0}", Encoding.Unicode.GetByteCount(s));
            //Console.WriteLine("Total mem len = {0}", b1 - b2);

            //--------------------- String inter pool
            // Простий вступ
            //string a = "hello world";
            //string b = a;
            //a = "hello";
            //Console.WriteLine("{0},{1}", a, b);
            //Console.WriteLine(a == b);
            //Console.WriteLine(ReferenceEquals(a, b));

            //Console.WriteLine((a + " world") == b);                 // returns 'True' because strings are equal
            //Console.WriteLine(ReferenceEquals((a + " world"), b));  // returns 'False' because operator + creates new string      

            // Додаємо вручну ссилку строку в intern pool, так як CLR додає туди ссилки тільки на літерали
            //string s1 = "hello";
            //string s2 = " world";
            //string s3 = s1 + s2;
            //string s4 = s1 + s2;

            //string.Intern(s4);
            //Console.WriteLine("Is interned : {0}", string.IsInterned(s4));

            ////Перевіряємо тепер заміну динамічної строки на літерал
            //string hello = "hello";
            //string helloWorld = "hello world";
            //string helloWorld2 = hello + " world";  

            //Console.WriteLine("{0}, {1} : {2}, {3}", helloWorld, helloWorld2,
            //                                        helloWorld == helloWorld2,
            //                                        ReferenceEquals(helloWorld, helloWorld2));

            //helloWorld2 = "hello world"; // якщо змінити тип змінної на object -> то '==' - false, ReferenceEquals - true бо йде comparison по ссилці
            //Console.WriteLine("{0}, {1} : {2}, {3}", helloWorld, helloWorld2,
            //                                        helloWorld == helloWorld2,
            //                                        ReferenceEquals(helloWorld, helloWorld2));

            // Тест на перевірку метода Intern - цей метод перевіряє чи строка в пулі і якщо так то повертає ссилку на цю строку
            // якщо ні то поміщає її туди і повертає ссилку на передану методу строку
            /* 1. створюємо об'єкт #1 типу string і присвоюємо ссилку на нього змінній s (об'єкт містить "abc")
               2. створюємо об'єкт #2 типу object який вказує на об'єкт типу string (об'єкт містить "abc")
               3. виклик метода string.Intern(o.ToString()) додає ссилку на об'єкт #2 типу стрінг в inter pool
               4. тепер коли об'єкт #2 типу стрінг є в intern pool то виклик string.Intern з "abc" буде вертати ссилку на об'єкт #2 типу стрінг
               5. коли викликається ReferenceEquals для o + string.Intern(s) вертається True так як string.Intern(s) вертає ссилку на об'єкт #2
               6. тепер створюється новий об'єкт #3 типу string який також містить "abc"
               7. виклик метода string.Intern(o2.ToString()) нічого не додає в intern pool в цьому випадку так як "abc" вже там 
                  вказує на об'єкт #2
               8. тепер останній ReferenceEquals вертає False так як порівнюються ссилки на об'єкт #3 i об'єкт #2
            */
            string s = new string(new char[] { 'a', 'b', 'c' });
            object o = string.Copy(s);

            Console.WriteLine(string.IsInterned(s) ?? "not in inter pool");
            Console.WriteLine(string.IsInterned(o.ToString()) ?? "not in inter pool");

            Console.WriteLine(ReferenceEquals(o, s));
            string.Intern(o.ToString());
            Console.WriteLine(ReferenceEquals(o, string.Intern(s)));

            object o2 = string.Copy(s);
            string.Intern(o2.ToString());     // ToString always returns reference to itself
            Console.WriteLine(ReferenceEquals(o2, string.Intern(s)));

            // IsInterned
            //string s = new string(new char[] { 'x', 'y', 'z' });
            //Console.WriteLine(string.IsInterned(s) ?? "not interned");
            //string.Intern(s);
            //Console.WriteLine(string.IsInterned(s) ?? "not interned");
            //Console.WriteLine(ReferenceEquals(string.IsInterned(new string(new char[] { 'x', 'y', 'z' })), s));

            //// LITERALS ARE INTERNED AUTOMATICALLY
            //Console.WriteLine(ReferenceEquals("xyz", s));

            //using(MyClass str = new MyClass(23) )
            //{
            //    str.a = 10;                
            //    Console.WriteLine(str.a);
            //}

            //MyStruct str = new MyStruct();
            //using (str)
            //{
            //    Console.WriteLine(str.a);
            //    str.Modify();
            //    Console.WriteLine(str.a);
            //}
            //Console.WriteLine(str.a);

            Console.ReadLine();
        }
    }

    struct MyStruct : IDisposable
    {
        public int a;
        public void Dispose()
        {
            Console.WriteLine("Disposing...");
        }
        public void Modify()
        {
            a++;
        }
    }

    class MyClass : IDisposable
    {
        public int a;
        public MyClass(int a)
        {
            this.a = a;
        }
        public void Dispose()
        {
            Console.WriteLine("Disposing...");
        }
    }
}
