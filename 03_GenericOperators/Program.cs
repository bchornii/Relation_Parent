using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _03_GenericOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Add(10.0, 12.0));
            Console.WriteLine(Add(1, 4));
            Console.WriteLine(Add(10.5M,10.0M));

            Console.ReadLine();
        }

        static T Add<T>(T a, T b)
        {
            // declare the parameters
            ParameterExpression paramA = Expression.Parameter(typeof(T), "a");
            ParameterExpression paramB = Expression.Parameter(typeof(T), "b");

            // add parameters 
            BinaryExpression body = Expression.Add(paramA, paramB);

            // compile it
            Func<T, T, T> add = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();

            // call it
            return add(a, b);
        }

        static T Sub<T>(T a, T b)
        {
            // declare the parameters
            ParameterExpression paramA = Expression.Parameter(typeof(T), "a");
            ParameterExpression paramB = Expression.Parameter(typeof(T), "b");

            return default(T);
        }
        
        
    }
}
