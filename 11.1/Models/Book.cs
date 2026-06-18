using System.ComponentModel.DataAnnotations;

namespace BooksInventoryCodeFirst.Models
{
    public class Book
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(100)]
        public string Publisher { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }
    }
}
