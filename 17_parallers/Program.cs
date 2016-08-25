using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _17_parallers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task<int>[] tasks = new Task<int>[]
            //{
            //    Task.Factory.StartNew(BookCar),
            //    Task.Factory.StartNew(BookHotel),
            //    Task.Factory.StartNew(BookPlane)
            //};
            //Task.WaitAll(tasks);

            //Func<int>[] methods = new Func<int>[] { BookCar, BookHotel, BookPlane };
            //List<Task<int>> tsks = new List<Task<int>>();
            //foreach (var item in methods)
            //{
            //    tsks.Add(Task.Factory.StartNew(item));
            //}
            //tsks.FirstOrDefault().ContinueWith(t => Console.WriteLine("the first one is done"))
            //                     .ContinueWith(t => Console.WriteLine("and thats all"))
            //                     .ContinueWith(t => Console.WriteLine("yes ))) all"));
            //Task.WaitAll(tsks.ToArray());

            Stopwatch w = new Stopwatch();
            w.Start();

            // Паралельно через task.waitall()
            //List<Task<int>> list = new List<Task<int>>();
            //Func<Task<int>>[] methods = new Func<Task<int>>[] { BookCarAsync, BookHotelAsync, BookPlaneAsync };
            //for (int i = 0; i < 3; i++)
            //{
            //    Task<int> t = methods[i]();
            //    list.Add(t);
            //}
            //Task.WaitAll(list.ToArray());

            // Такий же результат з більшим рівнем абстракції
            //Parallel.Invoke(() =>
            //{
            //    BookHotel();
            //},
            //() =>
            //{
            //    BookPlane();
            //},
            //() =>
            //{
            //    BookCar();
            //});

            w.Stop();
            Console.WriteLine("Elapsed time : " + w.ElapsedMilliseconds / 1000.0);

            Console.WriteLine("The end");
            Console.ReadLine();
        }

        static async Task DoBooking()
        {
            Console.WriteLine("Thead id = {0}",Thread.CurrentThread.ManagedThreadId);
            int x = await BookCarAsync();
            Console.WriteLine("Booking id ={0}", x);
            Console.WriteLine("Book is done.");
            Console.WriteLine("Thead id = {0}", Thread.CurrentThread.ManagedThreadId);
        }

        static int BookCar()
        {
            Console.WriteLine("Booking the car");
            Thread.Sleep(2000);
            Console.WriteLine("Car is booked.");
            return new Random().Next(100);
        }
        static Task<int> BookCarAsync()
        {
            return Task.Run(() =>
            {
                return BookCar();
            });
        }
        static int BookPlane()
        {
            Console.WriteLine("Booking the plane");
            Thread.Sleep(3000);
            Console.WriteLine("Plane is booked.");
            return new Random().Next(100);
        }
        static Task<int> BookPlaneAsync()
        {
            return Task.Run(() =>
            {
                return BookPlane();
            });
        }
        static int BookHotel()
        {
            Console.WriteLine("Booking the hotel");
            Thread.Sleep(4000);
            Console.WriteLine("Hotel is booked.");
            return new Random().Next(100);
        }
        static Task<int> BookHotelAsync()
        {
            return Task.Run(() =>
            {
                return BookHotel();
            });
        }
    }
}
