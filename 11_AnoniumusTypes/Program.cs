using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_AnoniumusTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            // anonimous type is immutable like struct, delegates and strings
            var peoples = new[]
            {
                 new { Name = "Holly", Age = 36},
                 new { Name = "Holly", Age = 36},
                 new { Name = "Holly", Age = 35}
            };

            foreach (var item in peoples)
            {
                Console.WriteLine(item.Name);
            }            

            Console.Read();
        }
    }
}
