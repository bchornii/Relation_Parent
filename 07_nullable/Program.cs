using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_nullable
{
    // Nullable<T> - є структурою, тобто типом значення. Це означає що для її перетворення
    // в ссилочний тип треба буде зробити упаковку. Екземпляр пакується в ссилку null якщо він немає значення
    // або пакується в значення типу T при наявності значення. Розпаковувати упаковане значення можна або в
    // нормальний тип або в відповідний тип який допускає null. Розпаковка null в нормальний тип викличе exception.

    /* Правила рівності при виклику first.Equals(second):
        1. якщо first немає значення і second має null - вони рівні
        2. якщо first немає значення і second має значення відмінне від null - вони не рівні(і навпаки)
        3. в іншому випадку вони рівні якщо значення first рівне значенню second
    */

    // Nullable типи можна зрівнювати з null

    // перетворення з T в T? неявне    
    // перетворення з T? в T явне
    // перетворення з null в T? неявне    
    // тобто існує неявне перетворення з int? в long?, int в long?; значення null типу int? перетворюються в null типу long?
    // якщо є перевантажені операції для типу T то тип T? отримує ті ж самі операції з дещо відмінним результатом
    // для унарних операцій тип також переходить в nullable i якщо будь який з операндів Null то повернтається null
    // операції еквівалентності тип який повертається залишається все ж таки bool

    class Program
    {
        static void Main(string[] args)
        {
            int? x = 5;            
            x = new int?(10);
            Console.WriteLine("Instance with value");
            Display(x);

            x = new int?();
            Console.WriteLine("Instance without value");
            Display(x);

            Console.WriteLine(new string('-', 80));

            int? nullable = 5;
            object boxed = nullable;            
            Console.WriteLine(boxed.GetType());

            int normal = (int)boxed;
            Console.WriteLine(normal);

            nullable = (int?)boxed;
            Console.WriteLine(nullable);

            nullable = new int?();
            boxed = nullable;
            Console.WriteLine(boxed == null);

            nullable = (int?)boxed;
            Console.WriteLine(nullable.HasValue);

            //Person p1 = new Person("alan tuk", new DateTime(1912, 6, 23), new DateTime(1954, 6, 7));
            //Person p2 = new Person("donald trump", new DateTime(1938, 1, 10), null);

            //Console.WriteLine(p1.Age.Days / 365);
            //Console.WriteLine(p2.Age.Days / 365);

            //int? parseInt = TryParse("3949348");
            //Console.WriteLine(parseInt??10);

            // до nullable типів можна застосовувати 'as' + 'is'

            int? aa = new int?();
            int? bb = null;

            object bx = aa;
            object bx1 = bb;
            int? unbx = bx as int?;            
            if(bx is int?)
            {
                Console.WriteLine(bx);
                Console.WriteLine(unbx);
            }

            int vv = aa ?? -1;
            Console.WriteLine(vv);

            int? vvv = TryParse("545");
            Console.WriteLine(vvv.HasValue ? vvv.Value.ToString() : "no value");

            Console.WriteLine(new string('-',80));
            int? v1 = null; // new int?();
            int? v2 = 2;
            int v3 = v2.Value - 2;
            int? v4 = v2 - 2;
            bool? v5 = null;
            bool? v6 = v5 | v5;
            //int v7 = v2 ?? v2;
            int v7 = v2 ?? 2;
            int? v8 = MyM();

            Console.WriteLine(10 == null);
            Console.WriteLine(v2 - v1);

            int[] values = { 7, 8, 9 };
            Action act = null;

            foreach (var item in values)
            {
                if(act == null) act = () => Console.WriteLine("First value : " + item);
            }

            act();
            
            Console.ReadLine();
        }

        public static int? MyM()
        {
            int? a = null;
            return a ?? null;
        }

        static void Display(int? x)
        {
            Console.WriteLine("Has value : " + x.HasValue);
            if (x.HasValue)
            {
                Console.WriteLine("Has value : " + x.Value);
                Console.WriteLine("Explicit conversion : " + (int)x);
            }
            Console.WriteLine("GetValueOrDefault() : {0}", x.GetValueOrDefault());
            Console.WriteLine("GetValueOrDefault() : {0}", x.GetValueOrDefault(10));
            Console.WriteLine("ToString() : \"{0}\"", x.ToString());
            Console.WriteLine("GetHashCode() : {0}", x.GetHashCode());
            Console.WriteLine();
        }

        // when using nullable type for with TryXXX it's much readable because
        // return value inscapsulate values about status (HasValue) and about value itself
        static int? TryParse(string text)
        {
            int ret;
            if(int.TryParse(text,out ret))
            {
                return ret;
            }
            else
            {
                return null;
            }
        }
    }

    class Person
    {
        DateTime birth;
        DateTime? death;
        string name;

        //public TimeSpan Age
        //{
        //    get
        //    {
        //        if(death == null)
        //        {
        //            return DateTime.Now - birth;
        //        }
        //        else
        //        {
        //            return death.Value - birth;
        //        }
        //    }
        //}

        public TimeSpan Age
        {
            get { return (death ?? DateTime.Now) - birth; }
        }

        public Person(string name, DateTime birth, DateTime? death)
        {
            this.birth = birth;
            this.death = death;
            this.name = name;
        }
    }

    public class PartialComparer
    {
        public static int? Compare<T>(T a,T b)
        {
            return Compare(Comparer<T>.Default, a, b);
        }
        public static int? Compare<T>(IComparer<T> comparer,T a,T b)
        {
            int ret = comparer.Compare(a, b);
            return ret == 0 ? new int?() : ret;
        }
        public static int? ReferenceCompare<T>(T first,T second) where T : class
        {
            return  first == second ? 0
                  : first == null ? -1
                  : second == null ? 1
                  : new int?();
        }
    }
}
