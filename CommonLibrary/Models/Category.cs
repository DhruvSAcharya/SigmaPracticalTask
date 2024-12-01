using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public CategoryName CategoryName { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum CategoryName
    {
        Electronics = 1 ,
        Furniture,
        Clothing,
        Books
    }
}
