using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_ExpressionVsDelegates
{
    public class BooksContext : DbContext
    {
        static BooksContext()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<BooksContext>());
        }

        public BooksContext() : base("name=BooksApiContext")
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
