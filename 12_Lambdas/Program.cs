using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Lambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            var films = new List<Film>
            {
                new Film { Name = "Rembo", Year = 1999 },
                new Film { Name = "Tutsi", Year = 2001 },
                new Film { Name = "Patric Star", Year = 2033 }
            };

            Action<Film> print = film => Console.WriteLine("Film = {0}, Year = {1}", film.Name, film.Year);
            films.ForEach(print);
            Console.WriteLine(new string('-', 80));
            
            films.FindAll(film => film.Year < 2000)
                 .ForEach(print);
            Console.WriteLine(new string('-', 80));
            
            films.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));
            films.ForEach(print);

            Console.Read();
        }
    }

    class Film
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
