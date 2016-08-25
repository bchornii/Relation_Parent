using System;
using System.Collections.Generic;

namespace _010_AutoProperties
{
    class Program
    {
        public static readonly List<Person> ConstCollection = new List<Person>
        {
            new Person { Age = 12, Name = "Adrian", Home = {Country = "UK", City = "Reading"} },
            new Person { Age = 15, Name = "Alex", Home = { Country = "UK", City = "Reading"} }
        };        
        static void Main(string[] args)
        {            
            foreach (var item in ConstCollection)
            {
                Console.WriteLine("Person : " + item.Age + " " + item.Name);
            }            

            Console.WriteLine(new string('-', 80));
            Person Tom = new Person()
            {
                Age = 19,
                Name = "Tom",
                Home = { Country = "UK", City = "Reading" },
                Friends =
                {
                    new Person { Age = 12, Name = "Adrian", Home = {Country = "UK", City = "Reading"} },
                    new Person { Age = 15, Name = "Alex", Home = { Country = "UK", City = "Reading"} }                    
                }
            };

            Console.WriteLine("Person : " + Tom.Age + " " + Tom.Name);
            Console.WriteLine("Location : " + Tom.Home.Country + " " + Tom.Home.City);
            Console.WriteLine("Friends : ");
            foreach (var item in Tom.Friends)
            {
                Console.WriteLine("Person : " + item.Age + " " + item.Name);
                Console.WriteLine("Location : " + item.Home.Country + " " + item.Home.City);
            }

            Console.ReadLine();
        }
    }

    class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        List<Person> friends = new List<Person>();
        public List<Person> Friends { get { return friends; } }

        Location home = new Location();
        public Location Home { get { return home; } }

        public Person() { }
        public Person(string name)
        {
            Name = name;
        }
    }
    class Location
    {
        public string Country { get; set; }
        public string City { get; set; }
    }
}
