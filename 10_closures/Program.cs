using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_closures
{
    delegate Base CovariantDel();
    delegate void ContravarianceDel(Derived parameter);
    delegate Base CoContraDel(Derived parameter);

    class Program
    {        
        static void Main(string[] args)
        {
            Action[] instances = new Action[10];

            int x;
            int z;
            for (int i = 0; i < instances.Length; i++)
            {
                x = 0;
                z = 0;
                int y = 0;
                instances[i] = () =>
                {
                    Console.WriteLine("x ={0}, y={1}", x, y, z);
                    x++;
                    y++;
                    z++;                    
                };
            }

            instances[0]();
            instances[0]();
            instances[0]();
            instances[1]();
            instances[0]();
            x = 10;
            instances[2]();

            Console.ReadLine();
        }

        static Derived GetSomeValue()
        {
            return new Derived();
        }
        static void SetSomeValue(Base parameter)
        {

        }
        static Derived GetSetSomeValue(Base parameter)
        {
            return new Derived();
        }
    }
    class Base
    {

    }
    class Derived : Base
    {

    }
}
