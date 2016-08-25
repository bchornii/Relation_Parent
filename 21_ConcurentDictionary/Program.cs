using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace _21_ConcurentDictionary
{
    class Program
    {
        static ConcurrentDictionary<string, CityInfo> cities = new ConcurrentDictionary<string, CityInfo>();
        static void Main(string[] args)
        {
            CityInfo[] data =
            {
                new CityInfo(){ Name = "Boston", Latitude = 42.358769M, Longitude = -71.057806M, RecentHighTemperatures = new int[] {56, 51, 52, 58, 65, 56,53}},
                new CityInfo(){ Name = "Miami", Latitude = 25.780833M, Longitude = -80.195556M, RecentHighTemperatures = new int[] {86,87,88,87,85,85,86}},
                new CityInfo(){ Name = "Los Angeles", Latitude = 34.05M, Longitude = -118.25M, RecentHighTemperatures =   new int[] {67,68,69,73,79,78,78}},
                new CityInfo(){ Name = "Seattle", Latitude = 47.609722M, Longitude =  -122.333056M, RecentHighTemperatures =   new int[] {49,50,53,47,52,52,51}},
                new CityInfo(){ Name = "Toronto", Latitude = 43.716589M, Longitude = -79.340686M, RecentHighTemperatures =   new int[] {53,57, 51,52,56,55,50}},
                new CityInfo(){ Name = "Mexico City", Latitude = 19.432736M, Longitude = -99.133253M, RecentHighTemperatures =   new int[] {72,68,73,77,76,74,73}},
                new CityInfo(){ Name = "Rio de Janiero", Latitude = -22.908333M, Longitude = -43.196389M, RecentHighTemperatures =   new int[] {72,68,73,82,84,78,84}},
                new CityInfo(){ Name = "Quito", Latitude = -0.25M, Longitude = -78.583333M, RecentHighTemperatures =   new int[] {71,69,70,66,65,64,61}}
            };

            // Add tasks
            Task[] tasks = new Task[2];

            tasks[0] = Task.Run(() =>
            {
                for (int i = 0; i < 2; i++)
                {
                    if (cities.TryAdd(data[i].Name, data[i]))
                    {
                        Console.WriteLine("Added {0} on thread {1}", data[i], Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        Console.WriteLine("Could not add {0}", data[i]);
                    }
                }
            });

            tasks[1] = Task.Run(() =>
            {
                for (int i = 2; i < data.Length; i++)
                {
                    if (cities.TryAdd(data[i].Name, data[i]))
                    {
                        Console.WriteLine("Added {0} on thread {1}", data[i], Thread.CurrentThread.ManagedThreadId);
                    }
                    else
                    {
                        Console.WriteLine("Could not add {0}", data[i]);
                    }
                }
            });

            // Wait for complete of all tasks
            Task.WaitAll(tasks);

            // Enumerate collection from main thread
            foreach (var city in cities)
            {
                Console.WriteLine("{0} has been added.", city.Key);
            }

            Console.WriteLine(new string('-', 80));

            // Add or update logic
            AddOrUpdateWihoutRetrieving();            

            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }

        private static void AddOrUpdateWihoutRetrieving()
        {
            // Sometime later. We receive new data from some source.
            CityInfo ci = new CityInfo()
            {
                Name = "Toronto",
                Latitude = 43.716589M,
                Longitude = -79.340686M,
                RecentHighTemperatures = new int[] { 54, 59, 67, 82, 87, 55, -14 }
            };

            // Try to add data. If it doesn't exists the object ci added. If it does
            // already exists, update existingVal according to the custom logic in the delegate
            cities.AddOrUpdate(ci.Name, ci, (key, val) =>
            {
                val.Latitude = ci.Latitude;
                val.Longitude = ci.Longitude;
                val.lastQueryDate = DateTime.Now;
                val.RecentHighTemperatures = ci.RecentHighTemperatures;
                return val;
            });

            GetCityTemperatures(ci.Name);
        }

        private static void GetCityTemperatures(string city_name)
        {
            Console.WriteLine("The most resent high temperatures for {0} are : ", cities[city_name].Name);
            Console.WriteLine(string.Join(",", cities[city_name].RecentHighTemperatures));
        }

        private static void RetrieveValueOrAdd()
        {
            string searchKey = "Caracas";
            CityInfo city = null;

            try
            {
                city = cities.GetOrAdd(searchKey, GetDataForCity(searchKey));
                GetCityTemperatures(city.Name);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Assume this method knows how to find long/lat/temp info for any specified city.
        static CityInfo GetDataForCity(string name)
        {
            // Real implementation left as exercise for the reader.
            if (string.CompareOrdinal(name, "Caracas") == 0)
                return new CityInfo()
                {
                    Name = "Caracas",
                    Longitude = 10.5M,
                    Latitude = -66.916667M,
                    RecentHighTemperatures = new int[] { 91, 89, 91, 91, 87, 90, 91 }
                };
            else if (string.CompareOrdinal(name, "Buenos Aires") == 0)
                return new CityInfo()
                {
                    Name = "Buenos Aires",
                    Longitude = -34.61M,
                    Latitude = -58.369997M,
                    RecentHighTemperatures = new int[] { 80, 86, 89, 91, 84, 86, 88 }
                };
            else
                throw new ArgumentException("Cannot find any data for {0}", name);
        }
    }

    class CityInfo : IEqualityComparer<CityInfo>
    {
        public string Name { get; set; }
        public DateTime lastQueryDate { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int[] RecentHighTemperatures { get; set; }

        public CityInfo(string name, decimal longitude, decimal latitude, int[] temps)
        {
            Name = name;
            lastQueryDate = DateTime.Now;
            Longitude = longitude;
            Latitude = latitude;
            RecentHighTemperatures = temps;
        }

        public CityInfo()
        {
        }

        public CityInfo(string key)
        {
            Name = key;
            // MaxValue means "not initialized"
            Longitude = decimal.MaxValue;
            Latitude = decimal.MaxValue;
            lastQueryDate = DateTime.Now;
            RecentHighTemperatures = new int[] { 0 };

        }
        public bool Equals(CityInfo x, CityInfo y)
        {
            return x.Name == y.Name && x.Longitude == y.Longitude && x.Latitude == y.Latitude;
        }

        public int GetHashCode(CityInfo obj)
        {
            CityInfo ci = obj;
            return ci.Name.GetHashCode();
        }
    }
}
