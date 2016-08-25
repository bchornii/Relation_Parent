using System;
using System.Collections.Generic;

namespace _06_equals
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.AddRange(new[] { "Hello", "my", "danger", "world" });
            Console.WriteLine(list.Contains("my"));

            List<Person> personList = new List<Person>();
            Person mike = new Person { Age = 19, Name = "Mike" };
            Person alison = new Person { Age = 22, Name = "Digi" };
            Person jane = new Person { Age = 34, Name = "Jane" };
            Person mike2 = new Person { Age = 19, Name = "Mike" };

            personList.AddRange(new[] { mike, alison, jane });
            personList.Sort();
            //personList.Sort(Person.AgeDescComparer());

            foreach (var item in personList)
            {
                Console.WriteLine(item.Age + " : " + item.Name);
            }

            Console.WriteLine(personList.Contains(mike2));

            Console.WriteLine(ReferenceEquals(mike,mike2));
            Console.WriteLine(mike.Equals(mike2));

            Console.WriteLine(null == mike);

            Console.ReadLine();
        }
    }

    class Person : IEquatable<Person>, IComparable<Person>
    {        
        public int Age { get; set; }
        public string Name { get; set; }

        private class AgeDescendingComparer : IComparer<Person>
        {
            public int Compare(Person x, Person y)
            {
                if(x.Age < y.Age)
                {
                    return 1;
                }
                if(x.Age > y.Age)
                {
                    return -1;
                }
                return 0;
            }
        }

        public static IComparer<Person> AgeDescComparer()
        {
            return new AgeDescendingComparer();
        }

        int IComparable<Person>.CompareTo(Person other)
        {
            if (Age > other.Age)
            {
                return 1;
            }
            if (Age < other.Age)
            {
                return -1;
            }
            return 0;
        }

        public bool Equals(Person other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;            
            return Age.Equals(other.Age) && Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {          
            return !(obj is Person) ? false : Equals(obj as Person);
        }

        public override int GetHashCode()
        {
            const int prime = 397;
            int result = Age;
            return (result * prime) ^ (Name != null ? Name.GetHashCode() : 0);
        }

        public static bool operator ==(Person p1, Person p2)
        {
            if (ReferenceEquals(p1, null)) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return !(p1 == p2);
        }
    }

   
}
