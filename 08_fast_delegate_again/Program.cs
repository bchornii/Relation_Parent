using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_fast_delegate_again
{
    delegate void MethodInvoker();

    class Program
    {
        static void Main(string[] args)
        {
            SomeClass c1 = new SomeClass();

            //c1.BaseEvent += baseEventHandler;
            //c1.DerivedEvent1 += baseEventHandler;
            //c1.DerivedEvent2 += baseEventHandler;

            //c1.CallEvents();

            MethodInvoker invoker = CreateDelegateInstance();
            invoker();
            invoker();

            //List<MethodInvoker> invList = new List<MethodInvoker>();
            //int counter = 0;
            //for (int i = 0; i < 5; i++)
            //{
            //    //int counter = i * 10;
            //    int ii = i;
            //    invList.Add(() =>
            //    {                    
            //        Console.WriteLine(counter + " : " + ii);
            //        counter++;
            //        ii++;
            //    });
            //}

            ////foreach (var item in invList)
            ////{
            ////    item();
            ////}

            //invList[0]();
            //invList[0]();
            //invList[0]();
            //invList[1]();
            //invList[1]();

            //invList[1]();
            //invList[1]();

            Console.ReadLine();

        }

        static MethodInvoker CreateDelegateInstance()
        {
            int counter = 5;
            MethodInvoker ret = () =>
            {
                Console.WriteLine(counter);
                counter++;
            };
            ret();
            return ret;
        }

        private static void baseEventHandler(BaseEvent EventObject)
        {
            Console.WriteLine("Event raised");
        }
    }

    class BaseEvent
    {

    }
    class Derived1_Event : BaseEvent
    {

    }
    class Derived2_Event : BaseEvent
    {

    }

    public delegate void EventDelegate<in T>(T EventObject);
    class SomeClass
    {
        EventDelegate<BaseEvent> baseEvent;
        EventDelegate<Derived1_Event> derived1_Event;
        EventDelegate<Derived2_Event> derived2_Event;

        public event EventDelegate<BaseEvent> BaseEvent
        {
            add
            {
                baseEvent += value;
            }
            remove
            {
                baseEvent -= value;
            }
        }

        public event EventDelegate<Derived1_Event> DerivedEvent1
        {
            add
            {
                derived1_Event += value;
            }
            remove
            {
                derived1_Event -= value;
            }
        }

        public event EventDelegate<Derived2_Event> DerivedEvent2
        {
            add
            {
                derived2_Event += value;
            }
            remove
            {
                derived2_Event -= value;
            }
        }

        public virtual void CallEvents()
        {
            baseEvent(new BaseEvent());
            derived1_Event(new Derived1_Event());
            derived2_Event(new Derived2_Event());            
        }
    }
}
