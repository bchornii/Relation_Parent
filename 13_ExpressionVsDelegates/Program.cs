using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _13_ExpressionVsDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check Expression<Func<T,T>> vs Func<T,T>
            //using(BooksContext context = new BooksContext())
            //{
            //    var books = GetWhere(context, b => b.BookId < 4, 5).ToList();
            //    foreach (var item in books)
            //    {
            //        Console.WriteLine(item.BookId + " " + item.Title);
            //    }                
            //}

            // вивід типів відбудеться в два етапи (розділ 9.4 C# in depth)
            PrintConvertedValue("I'm string", x => x.Length);           

            Console.ReadLine();
        }

        static IEnumerable<Book> GetWhere(BooksContext context, Expression<Func<Book,bool>> where,int numberOfItems)
        {
            //var pred = where.Compile();
            return context.Books
                          .Where(where)
                          .OrderByDescending(b => b.BookId)
                          .Take(numberOfItems);                    
        }

        static void PrintConvertedValue<TSource,TOutput>(TSource input,Converter<TSource,TOutput> converter)
        {
            Console.WriteLine(converter(input));
        }
    }
}
