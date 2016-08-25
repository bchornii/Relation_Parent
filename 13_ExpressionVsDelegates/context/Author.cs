using System.ComponentModel.DataAnnotations;

namespace _13_ExpressionVsDelegates
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
