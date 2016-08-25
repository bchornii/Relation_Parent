using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_1_DelegateAndEvents
{
    class FieldLikeEventClass
    {
        public event EventHandler someEvent;

        public void CallSomeEvent()
        {
            if(someEvent != null)
            {
                someEvent(this, new EventArgs());
            }
        }
    }

    delegate T CoDelegate<out T>();
    delegate void ContraDelegate<in T>();

    class Program
    {
        static void Main(string[] args)
        {
            // covariance in generic delegates = you can assign delegate with mode derived param type to less derived
            //CoDelegate<Base> some_co_del_inst = SomeCoMethod1;
            //CoDelegate<Derived> some_co_del_inst1 = SomeCoMethod1;
            //some_co_del_inst = some_co_del_inst1;
            //some_co_del_inst();
            //some_co_del_inst1();

            // contravarianca in generic delegates = you can assign delegate with less derived param type to more derived
            ContraDelegate<Base> some_contra_del_inst = SomeContraMethod1;
            ContraDelegate<Derived> some_contra_del_inst1 = SomeContraMethod1;
            some_contra_del_inst();
            some_contra_del_inst1();

            some_contra_del_inst1 = some_contra_del_inst;

            Console.ReadLine();
        }

        static Base SomeCoMethod()
        {
            Console.WriteLine("Some covariance method.");
            return new Base();
        }
        static Derived SomeCoMethod1()
        {
            Console.WriteLine("Some covariance method.");
            return new Derived();
        }

        static void SomeContraMethod()
        {
            Console.WriteLine("Some contravariance method.");
        }
        static void SomeContraMethod1()
        {
            Console.WriteLine("Some contravariance method");
        }
    }

    class Base { }
    class Derived : Base { }
}
