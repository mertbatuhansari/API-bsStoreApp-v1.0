using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Book
    {        
        public int Id { get; set; }        
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Title { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public decimal Price { get; set; }
    }
}
