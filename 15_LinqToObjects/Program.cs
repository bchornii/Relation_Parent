using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_LinqToObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList list = new ArrayList { "First", "Second", "Third" };
            IEnumerable<string> strings = list.Cast<string>();

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            list = new ArrayList { 1, 3, 2 };
            IEnumerable<int> ints = list.Cast<int>();
            foreach (var item in ints)
            {
                Console.WriteLine(item);
            }

            // short cannot be casted to int
            //List<short> shortList = new List<short> { 1, 2, 3, 4 };
            //ints = shortList.Cast<int>();
            //foreach (var item in ints)
            //{
            //    Console.WriteLine(item);
            //}

            // на нетипізованій колекції ми не можемо визвати Select
            Console.WriteLine(new string('-', 80));
            foreach (var item in list.Cast<int>().Select(i => i * i))
            {
                Console.WriteLine(item);
            }

            // приведення до типу перед використанням колекції
            Console.WriteLine(new string('-', 80));
            List<IInterface> myList = new List<IInterface> { new MyClass { A = 1,  N ="Bla" },
                                                             new MyClass { A = 4,  N ="Gla" },
                                                             new MyClass { A = 12, N ="Ila" },
                                                             new MyClass { A = 32, N ="Bla" },
                                                             new MyClass { A = 32, N ="Ala" }};

            List<IInterface> myList1 = new List<IInterface> { new MyClass { A = 1,  N ="Bla1" },
                                                              new MyClass { A = 4,  N ="Gla1" },
                                                              new MyClass { A = 22, N ="Ila1" },
                                                              new MyClass { A = 31, N ="Bla1" },
                                                              new MyClass { A = 32, N ="Ala1" }};

            var myListElements = myList.Cast<MyClass>().Select(m => m.A);
            foreach (var item in myListElements)
            {
                Console.WriteLine(item);
            }

            // застосування декілької where
            Console.WriteLine(new string('-', 80));
            var myFilteredList = myList.Cast<MyClass>()
                                       .Where(m => m.A > 0)
                                       .Where(m => m.A > 1)
                                       .Where(m => m.A < 20)
                                       .Select(m => m.A * m.A);
            foreach (var item in myFilteredList)
            {
                Console.WriteLine(item);
            }

            // сортування даних 
            Console.WriteLine(new string('-', 80));
            var mySortedList = myList.Cast<MyClass>()
                                     .Where(m => m.A > 0)
                                     .OrderByDescending(m => m.A)
                                     .ThenBy(m => m.N)
                                     .Select(m => new { A = m.A, N = m.N });
            foreach (var item in mySortedList)
            {
                Console.WriteLine(item.A + " : " + item.N);
            }

            // змінна let
            Console.WriteLine(new string('-', 80));
            var myLet = from m in myList.Cast<MyClass>()
                        let Number = m.A
                        let Name = m.N
                        orderby Number
                        select new { Num = Number, SomeName = Name };
            foreach (var item in myLet)
            {
                Console.WriteLine(item);
            }

            // let в термінах розширюючих методів
            Console.WriteLine(new string('-', 80));
            var myLetInMethod = myList.Cast<MyClass>()
                                      .Select(u => new { u, u.A })
                                      .OrderBy(z => z.A)
                                      .Select(z => new { Name = z.u.N, Number = z.A });
            foreach (var item in myLetInMethod)
            {
                Console.WriteLine(item);
            }

            // inner JOIN через LINQ
            Console.WriteLine(new string('-', 80));
            var myInnerJoinedList = from m in myList.Cast<MyClass>()
                                    join m1 in myList1.Cast<MyClass>()
                                      on m.A equals m1.A
                                    select new { m_A = m.A, m1_A = m1.A, m_Name = m.N, m1_Name = m1.N };

            foreach (var item in myInnerJoinedList)
            {
                Console.WriteLine("M.A = {0}, M1.A = {1}, M.Name = {2}, M1.Name = {3}", item.m_A, item.m1_A, item.m_Name, item.m1_Name);
            }

            // inner join фільтрована колекція (простіше якшо лівою є колекція яку треба сортувати + вигідніше сортувати до join)
            Console.WriteLine(new string('-', 80));
            var myInnerFilteredList = from m in myList.Cast<MyClass>()
                                      where m.A > 10
                                      join m1 in myList1.Cast<MyClass>()
                                        on m.A equals m1.A
                                      select new { mA = m.A, m1A = m1.A, mN = m.N, m1N = m1.N };

            var myInnerFilteredList1 = from m1 in myList1.Cast<MyClass>()
                                       join m in (from m in myList.Cast<MyClass>()
                                                  where m.A > 10
                                                  select m)
                                         on m1.A equals m.A
                                       select new { m_A = m.A, m1_A = m1.A, m_Name = m.N, m1_Name = m1.N };

            foreach (var item in myInnerFilteredList1)
            {
                Console.WriteLine(item);
            }

            // зєднання для робітників і менеджерів           
            List<Human> employees = new List<Human>
            {
                new Employee { ID = 1, Age = 23, Name = "Oles", ManagerID = 1 },
                new Employee { ID = 2, Age = 18, Name = "Alex", ManagerID = 2 },
                new Employee { ID = 3, Age = 34, Name = "Ivan", ManagerID = 1 },
                new Employee { ID = 4, Age = 22, Name = "Vasil", ManagerID = 2 },
                new Employee { ID = 5, Age = 23, Name = "Nazar", ManagerID = 1 },
                new Employee { ID = 6, Age = 54, Name = "Valentin", ManagerID = 2 },
                new Employee { ID = 7, Age = 33, Name = "Boris", ManagerID = 1 }
            };
            List<Human> managers = new List<Human>
            {
                new Manager { ID = 1, Age = 25, Name = "Bogdan", Employees = new List<int> { 1,3,5,7 } },
                new Manager { ID = 2, Age = 32, Name = "Roman", Employees = new List<int> { 2, 4 } },
                new Manager { ID = 3, Age = 40, Name = "Valera" }
            };

            // inner join менеджерів і робітників
            Console.WriteLine("1" + new string('-', 79));
            var empMangInnerJoin = from e in employees.Cast<Employee>()
                                   join m in managers.Cast<Manager>()
                                     on e.ManagerID equals m.ID
                                   orderby e.ID
                                   select new { ManagerId = m.ID, ManagerName = m.Name, EmpId = e.ID, EmpName = e.Name };

            foreach (var item in empMangInnerJoin)
            {
                Console.WriteLine("Employee ID = {0}, manager id = {1}, employee name = {2}, manager name = {3}",
                                item.EmpId, item.ManagerId, item.EmpName, item.ManagerName);
            }

            // inner join через розшивюючі методи
            Console.WriteLine("2" + new string('-', 79));
            var empMangInnerJoinExtMethods = employees.Cast<Employee>()
                                                      .Join(managers.Cast<Manager>(),
                                                            e => e.ManagerID,
                                                            m => m.ID,
                                                            (e, m) => new { ManagerId = m.ID, ManagerName = m.Name, EmpId = e.ID, EmpName = e.Name })
                                                      .OrderBy(z => z.EmpId);

            foreach (var item in empMangInnerJoinExtMethods)
            {
                Console.WriteLine("Employee ID = {0}, manager id = {1}, employee name = {2}, manager name = {3}",
                                item.EmpId, item.ManagerId, item.EmpName, item.ManagerName);
            }

            // групове зєднання менеджера з робітниками (групове з'єднання виводить навіть ті елементи лівої таблиці які не мають
            // відповідних у правій таблиці - це як left outer join)
            Console.WriteLine("3" + new string('-', 79));
            var empMangInnerIntoJoin = from m in managers.Cast<Manager>()
                                       join e in employees.Cast<Employee>()
                                         on m.ID equals e.ManagerID into allEmployees
                                       where allEmployees.Count() > 1
                                       select new { ManagerId = m.ID, ManagerName = m.Name, AllEmployees = allEmployees };

            foreach (var manager in empMangInnerIntoJoin)
            {
                Console.WriteLine("Manager id = {0}, name = {1}", manager.ManagerId, manager.ManagerName);
                foreach (var emp in manager.AllEmployees)
                {
                    Console.WriteLine(" Employee id = {0}, name = {1}", emp.ID, emp.Name);
                }
            }

            // теж саме тільки з group join
            Console.WriteLine("4" + new string('-', 79));
            var empManagerOuterJoin = managers.Cast<Manager>()
                                              .GroupJoin(employees.Cast<Employee>(),
                                                         m => m.ID,
                                                         e => e.ManagerID,
                                                         (m, emps) => new { ManagerId = m.ID, ManagerName = m.Name, AllEmployees = emps });
            //.Where(z => z.AllEmployees.Count() > 1);

            foreach (var manager in empManagerOuterJoin)
            {
                Console.WriteLine("Manager id = {0}, name = {1}", manager.ManagerId, manager.ManagerName);
                foreach (var emp in manager.AllEmployees)
                {
                    Console.WriteLine(" Employee id = {0}, name = {1}", emp.ID, emp.Name);
                }
            }

            // генерування кількості працівників для кожного менеджера
            Console.WriteLine("5" + new string('-', 79));
            var mngEmpsCount = from m in managers.Cast<Manager>()
                               join e in employees.Cast<Employee>()
                                 on m.ID equals e.ManagerID into AllEmployees
                               select new { ManagerID = m.ID, ManagerName = m.Name, EmpsCount = AllEmployees.Count() };


            foreach (var item in mngEmpsCount)
            {
                Console.WriteLine("Manager ID = {0}, manager name = {1}, employees number = {2}",
                    item.ManagerID, item.ManagerName, item.EmpsCount);
            }

            // тож саме тільки з розширюючими методами
            Console.WriteLine("6" + new string('-', 79));
            var msgEmpsCountExpMethods = managers.Cast<Manager>()
                                                 .GroupJoin(employees.Cast<Employee>(),
                                                  m => m.ID,
                                                  e => e.ManagerID,
                                                  (m, emps) => new { ManagerID = m.ID, ManagerName = m.Name, EmpsCount = emps.Count() });
            foreach (var item in msgEmpsCountExpMethods)
            {
                Console.WriteLine("Manager ID = {0}, manager name = {1}, employees number = {2}",
                    item.ManagerID, item.ManagerName, item.EmpsCount);
            }

            // cross join
            Console.WriteLine("7" + new string('-', 79));
            var someSequence = from m in managers.Cast<Manager>()
                               from ml in myList.Cast<MyClass>()
                               select new { ManagerId = m.ID, SomeValue = ml.A };
            foreach (var item in someSequence)
            {
                Console.WriteLine("Manager id = {0}, some value = {1}", item.ManagerId, item.SomeValue);
            }

            // cross join коли права послідовність залежить від лівої послідовності
            Console.WriteLine("8" + new string('-', 79));
            var fooSequence = from n in Enumerable.Range(1, 4)
                              from m in Enumerable.Range(11, n)
                              select new { Left = n, Right = m };
            foreach (var item in fooSequence)
            {
                Console.WriteLine("Left = {0}, right = {1}", item.Left, item.Right);
            }

            // cross join коли права послідовність залежить від лівої послідовності
            // через розширюючі методи
            // *** також треба зазначити що SelectMany має потокове виконання, порівнюючи з внутрішніми і груповими
            // зєднаннями - де права таблиця завантажується одразу, тут права таблиця буде заново генеруватись для 
            // кожного елемента з лівої таблиці ***
            Console.WriteLine("9" + new string('-', 79));
            var fooSequenceExtMethods = Enumerable.Range(1, 4)
                                                  .SelectMany(n => Enumerable.Range(11, n),
                                                             (n, m) => new { Left = n, Right = m });

            foreach (var item in fooSequenceExtMethods)
            {
                Console.WriteLine("Left = {0}, right = {1}", item.Left, item.Right);
            }

            // cross join для працівників і менеджерів
            Console.WriteLine("9.1" + new string('-', 77));
            var empMngsSelMany = managers.Cast<Manager>()
                                         //.Where(m => m.ID == 1)
                                         .SelectMany(m => employees.Cast<Employee>()
                                                                   .Where(e => e.ManagerID == m.ID)
                                                                   .Select(e => new { EmpId = e.ID, EmpName = e.Name }),
                                                     (m, e) => new { ManagerId = m.ID, EmpId = e.EmpId, EmpName = e.EmpName })
                                         .GroupBy(z => z.ManagerId,
                                                  z => new { EmpId = z.EmpId, EmpName = z.EmpName })
                                         .OrderByDescending(z => z.Key);

            foreach (var item in empMngsSelMany)
            {
                //Console.WriteLine("Manager id = {0} : Employee id = {1}, employee name = {2}",item.ManagerId, item.EmpId, item.EmpName);
                Console.WriteLine("Manager id = {0}",item.Key);
                foreach (var e in item)
                {
                    Console.WriteLine(" Employee id = {0}, employee name = {1}", e.EmpId, e.EmpName);
                }
            }

            // групування елементів
            // *** групування не виконується потоково воно буферизує елементи які відсортовані по ключу ***
            Console.WriteLine("10" + new string('-', 78));
            var groupedEmployees = from e in employees.Cast<Employee>()
                                   where e.ManagerID >= 1
                                   group e by e.ManagerID;
            foreach (var item in groupedEmployees)
            {
                Console.WriteLine("Manager id : {0}", item.Key);
                foreach (var emp in item)
                {
                    Console.WriteLine(" Employee id = {0}, employee name = {1}", emp.ID, emp.Name);
                }
            }

            // групування елементів на основі розширюючих методів
            Console.WriteLine("11" + new string('-', 78));
            var groupedEmployeesExtMethods = employees.Cast<Employee>()
                                                      .Where(e => e.ManagerID >= 1)
                                                      .GroupBy(e => e.ManagerID);
            foreach (var item in groupedEmployeesExtMethods)
            {
                Console.WriteLine("Manager id : {0}", item.Key);
                foreach (var emp in item)
                {
                    Console.WriteLine(" Employee id = {0}, employee name = {1}", emp.ID, emp.Name);
                }
            }

            // групування елементів вибираючи не сам елемент а його пропертю
            Console.WriteLine("12" + new string('-', 78));
            var groupedEmpsPropetry = from e in employees.Cast<Employee>()
                                      where e.ManagerID >= 1
                                      group e.ID by e.ManagerID;

            foreach (var item in groupedEmpsPropetry)
            {
                Console.WriteLine("Manager id = {0}", item.Key);
                foreach (var e in item)
                {
                    Console.WriteLine(" " + e);
                }
            }

            // теж саме через розширюючий метод
            Console.WriteLine("13" + new string('-', 78));
            var groupedEpmsPropExtMethods = employees.Cast<Employee>()
                                                     .GroupBy(e => e.ManagerID,
                                                              e => e.ID);
            foreach (var item in groupedEpmsPropExtMethods)
            {
                Console.WriteLine("Manager id = {0}", item.Key);
                foreach (var e in item)
                {
                    Console.WriteLine(" " + e);
                }
            }

            // продовження запиту
            Console.WriteLine("14" + new string('-', 78));
            var queryContinuation = from e in employees.Cast<Employee>()
                                    where e.ManagerID >= 0
                                    group e by e.ManagerID
                                    into groupedSeq
                                    select new { ManagerId = groupedSeq.Key, Count = groupedSeq.Count() };
            foreach (var item in queryContinuation)
            {
                Console.WriteLine("Manager id = {0}, emps count = {1}", item.ManagerId, item.Count);
            }

            // продовження запиту через розширюючі методи
            Console.WriteLine("15" + new string('-', 78));
            var queryContinuationExtMethods = employees.Cast<Employee>()
                                                       .Where(e => e.ManagerID >= 1)
                                                       .GroupBy(e => e.ManagerID)
                                                       .Select(z => new { ManagerId = z.Key, Count = z.Count() });
            foreach (var item in queryContinuationExtMethods)
            {
                Console.WriteLine("Manager id = {0}, emps count = {1}", item.ManagerId, item.Count);
            }



            Console.ReadLine();
        }
    }

    interface IInterface
    {

    }

    class MyClass : IInterface
    {
        public int A { get; set; }

        public string N { get; set; }
    }

    interface Human { }
    class Person : Human
    {
        public int ID { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    }
    class Employee : Person
    {
        public int ManagerID { get; set; }
    }
    class Manager : Person
    {
        public List<int> Employees { get; set; }
        public Manager()
        {
            Employees = new List<int>();
        }
    }
}
