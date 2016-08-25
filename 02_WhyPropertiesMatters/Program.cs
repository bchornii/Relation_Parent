using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_WhyPropertiesMatters
{
    // 1. cannot pass property like 'ref' in method
    // 2. when struct is a property of class object when you 'get' called it returns copy of struct value
    class Program
    {
        int value { get; set; }

        static void Main(string[] args)
        {
            // !ref for fields and properties
            //int a = 10;
            //// changeValue(ref value, 13); !!!!!!!!!!! wrong
            //changeValue(ref a, 24);
            //Console.WriteLine(a);

            // mutable struct
            MutableStructHolder holder = new MutableStructHolder();

            // affects the value of holder.Fields
            holder.Field.SetValue(10);

            // retrieves holder.property as a copy and changes the copy
            holder.Property.SetValue(10);

            Console.WriteLine(holder.Field.Value);
            Console.WriteLine(holder.Property.Value);            

            Console.ReadLine();
        }

        static void changeValue(ref int val,int toVal)
        {
            val = toVal;
        }
    }

    struct MutableStruct
    {
        public int Value { get; set; }
        public void SetValue(int newValue)
        {
            Value = newValue;
        }
    }

    class MutableStructHolder
    {
        public MutableStruct Field;
        public MutableStruct Property { get; set; }
    }
}
